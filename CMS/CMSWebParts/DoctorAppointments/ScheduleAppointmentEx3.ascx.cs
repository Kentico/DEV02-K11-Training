using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using CMS.EventLog;
using CMS.Helpers;
using CMS.PortalEngine.Web.UI;
using CMS.Base.Web.UI;

using DoctorAppointments;

public partial class CMSWebParts_DoctorAppointments_ScheduleAppointmentEx3 : CMSAbstractWebPart
{
    #region Variables

    private List<String> errors = new List<string>();

    #endregion

    #region Events

    protected override void OnLoad(EventArgs e)
    {
        // Register Semantic UI and jQuery on page where this web part is used
        // You can register scripts also on the Master page for them to be available globally
        CssRegistration.RegisterCssLink(this.Page, "https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.8/semantic.min.css");
        CssRegistration.RegisterCssLink(this.Page, "https://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css");

        ScriptHelper.RegisterScriptFile(this.Page, "https://code.jquery.com/jquery-2.2.0.min.js", false);
        ScriptHelper.RegisterScriptFile(this.Page, "https://code.jquery.com/ui/1.10.4/jquery-ui.min.js", false);
        ScriptHelper.RegisterScriptFile(this.Page, "https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.8/semantic.min.js", false);

        base.OnLoad(e);
    }

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    /// <summary>
    /// Initializes the control properties.
    /// </summary>
    protected void SetupControl()
    {
        if (StopProcessing)
        {
            // Do nothing
        }
        else
        {
            // Initialize doctor's sselect menu
            InitializeDoctorsDropdown();
        }
    }

    /// <summary>
    /// Handles submittion of an appointment
    /// </summary>
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (ValidateForm())
        {
            // Hide errors in case there are some still displayed
            HideErrors();

            // Inserts new appointment from form data
            InsertNewAppointment();
        }
        else
        {
            // show all errors
            ShowErrors(errors);
        }
    }

    /// <summary>
    /// Gets executed when selected value in doctor's dropdown changes
    /// </summary>
    protected void SelectDoctor_SelectedIndexChanged(object sender, EventArgs e)
    {
        // create custom SelectDoctorEventArgs
        var args = new SelectDoctorEventArgs()
        {
            DoctorID = ValidationHelper.GetInteger(SelectDoctor.SelectedValue, 0)
        };

        // raise component event
        ComponentEvents.RequestEvents.RaiseComponentEvent(sender, args, "DoctorDropdown", "SelectedIndexChanged");
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes doctor's dropdown list
    /// </summary>
    private void InitializeDoctorsDropdown()
    {
        // Try to get the data from cache
        var doctors = CacheHelper.Cache(
            cs =>
            {
                // Get all doctors
                var result = DoctorInfoProvider.GetDoctors()
                    .Columns("DoctorID", "DoctorFirstName", "DoctorLastName");

                // Setup the cache dependencies only when caching is active
                if (cs.Cached)
                {
                    // Flush the cache if any doctor is changed
                    cs.CacheDependency = CacheHelper.GetCacheDependency("DoctorAppointments.Doctor|all");
                }
                return result;
            },
            // cache for 10 minutes
            new CacheSettings(10, "DoctorList")
        );


        // Get all doctors
        foreach (var doctor in doctors)
        {
            // Add each doctor to dropdown list
            var listItem = new ListItem(String.Format("{0} {1}", doctor.DoctorFirstName, doctor.DoctorLastName), ValidationHelper.GetString(doctor.DoctorID, String.Empty));
            SelectDoctor.Items.Add(listItem);
        }
    }

    /// <summary>
    /// Displays errors on site
    /// </summary>
    private void ShowErrors(List<String> errors)
    {
        if (errors.Count > 0)
        {
            // bind Error list
            ErrorList.DataSource = errors;
            ErrorList.DataBind();
            plcErrorMessage.Visible = true;
        }
    }

    /// <summary>
    /// Displays error on site
    /// </summary>
    private void ShowErrors(string errorMessage)
    {
        // create error list
        var errorList = new List<String>();
        errorList.Add(errorMessage);

        // bind Error list 
        ErrorList.DataSource = errorList;
        ErrorList.DataBind();
        plcErrorMessage.Visible = true;

    }

    /// <summary>
    /// Hides errors on site
    /// </summary>
    private void HideErrors()
    {
        plcErrorMessage.Visible = false;
    }

    /// <summary>
    /// Shows success message
    /// </summary>
    private void ShowSuccessMessage()
    {
        plcSuccessMessage.Visible = true;
    }

    /// <summary>
    /// Checks if all information in form are valid
    /// </summary>
    /// <returns>True if all information in form is valid, false otherwise</returns>
    private bool ValidateForm()
    {
        bool isValid = true;
        errors = new List<String>();

        if (String.IsNullOrEmpty(FirstName.Value))
        {
            errors.Add("First name is required");
            isValid = false;
        }
        if (String.IsNullOrEmpty(LastName.Value))
        {
            errors.Add("Last name is required");
            isValid = false;
        }
        if (String.IsNullOrEmpty(Email.Value))
        {
            errors.Add("E-mail is required");
            isValid = false;
        }
        else
        {
            if (!ValidationHelper.IsEmail(Email.Value))
            {
                errors.Add("Invalid e-mail address");
                isValid = false;
            }
        }
        if (String.IsNullOrEmpty(DateOfBirth.Value))
        {
            errors.Add("Date of Birth is required");
            isValid = false;
        }
        if (String.IsNullOrEmpty(DateOfAppointment.Value))
        {
            errors.Add("Date of appointment is required");
            isValid = false;
        }
        if (String.IsNullOrEmpty(SelectDoctor.SelectedValue))
        {
            errors.Add("Please select a doctor");
            isValid = false;
        }
        if (!String.IsNullOrEmpty(TelephoneNumber.Value))
        {
            if (!ValidationHelper.IsUsPhoneNumber(TelephoneNumber.Value))
            {
                errors.Add("Please use valid U.S. phone number");
                isValid = false;
            }
        }
        return isValid;
    }

    /// <summary>
    /// Creates new AppointmentInfo 
    /// </summary>
    /// <returns>True if appointment was successfully created, false otherwise</returns>
    private void InsertNewAppointment()
    {
        try
        {
            // prepare appointment
            var appointment = new AppointmentInfo()
            {
                AppointmentPatientFirstName = FirstName.Value,
                AppointmentPatientLastName = LastName.Value,
                AppointmentPatientEmail = Email.Value,
                AppointmentPatientBirthDate = DateTime.ParseExact(DateOfBirth.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                AppointmentDate = DateTime.ParseExact(DateOfAppointment.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture),
                AppointmentDoctorID = ValidationHelper.GetInteger(SelectDoctor.SelectedValue, 0),
                AppointmentPatientPhoneNumber = TelephoneNumber.Value
            };

            // insert appointment
            AppointmentInfoProvider.SetAppointmentInfo(appointment); // or appointment.Insert();

            // Shows successful message to user
            ShowSuccessMessage();

            // Hide form
            plcAppointmentForm.Visible = false;
        }
        catch (Exception ex)
        {
            // log exception to Event log
            EventLogProvider.LogException("DoctorsAppointment", "CREATE", ex);

            // show error message to user
            ShowErrors(ex.Message);
        }
    }

    #endregion
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

using CMS.DataEngine;
using CMS.Helpers;
using CMS.PortalEngine.Web.UI;

using DoctorAppointments;

public partial class CMSWebParts_DoctorAppointments_AppointmentListEx2 : CMSAbstractWebPart
{
    #region Properties

    /// <summary>
    /// Gets value out of FilterDoctor property defined when creating/editing web part
    /// Value represents DoctorID
    /// </summary>
    private int FilterDoctor
    {
        get
        {
            return ValidationHelper.GetInteger(this.GetValue("FilterDoctor"), 0);
        }
        set { }
    }

    /// <summary>
    /// Represents returned columns
    /// </summary>
    private String FilterColumns
    {
        get
        {
            return "DoctorID, DoctorFirstName, DoctorLastName, AppointmentPatientFirstName, AppointmentPatientLastName, AppointmentDate";
        }
    }

    #endregion

    #region Events

    /// <summary>
    /// Content loaded event handler.
    /// </summary>
    public override void OnContentLoaded()
    {
        base.OnContentLoaded();
        SetupControl();
    }

    #endregion

    #region Methods

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
            // load appointments with joined doctors
            var appointments = LoadAppointments(DateTime.Now);

            // assign appointments to repeater
            repAppointments.DataSource = appointments;
            repAppointments.DataBind();
        }
    }

    /// <summary>
    /// Loads the apppointments joined with doctor at given date
    /// </summary>
    /// <param name="appointmentsDate">Appointments will be loaded on this date</param>
    /// <returns>List of appointments joined with doctor</returns>
    private List<AppointmentDoctorModel> LoadAppointments(DateTime appointmentsDate)
    {
        // join DoctorAppointments_Appointment with DoctorAppointments_Doctor
        var appointmentsWithDoctors = AppointmentInfoProvider.GetAppointments()
            .Columns(FilterColumns)
            .Source(m => m.LeftJoin<DoctorInfo>("AppointmentDoctorID", "DoctorID"))
            .Where("AppointmentDoctorID", QueryOperator.Equals, FilterDoctor);

        // execute query and get data set
        var ds = appointmentsWithDoctors.Execute();

        // convert DataSet int AppointmentDoctorModel object 
        return ConvertDataSet(ds);
    }

    #endregion

    #region AppointmentDoctorModel class

    /// <summary>
    /// Private class representing an appointment joined with doctor
    /// </summary>
    private class AppointmentDoctorModel
    {
        public int DoctorID { get; set; }
        public string DoctorFirstName { get; set; }
        public string DoctorLastName { get; set; }
        public string DoctorFullName
        {
            get
            {
                return String.Format("{0} {1}", DoctorFirstName, DoctorLastName);
            }
        }
        public string AppointmentPatientFirstName { get; set; }
        public string AppointmentPatientLastName { get; set; }
        public string AppointmentPatientFullName
        {
            get
            {
                return String.Format("{0} {1}", AppointmentPatientFirstName, AppointmentPatientLastName);
            }
        }
        public DateTime AppointmentDate { get; set; }
    }

    #endregion

    #region Helper methods

    /// <summary>
    /// Converts a DataSet to typed object list
    /// </summary>
    /// <typeparam name="dataSet">Input dataset</typeparam>
    /// <returns>List with AppointmentDoctorModel objects</returns>
    private List<AppointmentDoctorModel> ConvertDataSet(DataSet dataSet) 
    {
        return dataSet.Tables[0].AsEnumerable().Select(dr => new AppointmentDoctorModel()
        {
            DoctorID = dr.Field<int>("DoctorID"),
            DoctorFirstName = dr.Field<string>("DoctorFirstName"),
            DoctorLastName = dr.Field<string>("DoctorLastName"),
            AppointmentPatientFirstName = dr.Field<string>("AppointmentPatientFirstName"),
            AppointmentPatientLastName = dr.Field<string>("AppointmentPatientLastName"),
            AppointmentDate = dr.Field<DateTime>("AppointmentDate"),
        }).ToList();
    }

    #endregion
}
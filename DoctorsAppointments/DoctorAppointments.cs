using CMS;
using CMS.DataEngine;
using DoctorAppointments;

[assembly: RegisterModule(typeof(DoctorAppointmentsModule))]
 
public class DoctorAppointmentsModule : Module
{
    // Module class constructor, inherits from the base constructor with the code name of the module as the parameter
    public DoctorAppointmentsModule() : base("DoctorAppointments")
    {
    }

    /// <summary>
    /// Initializes the module. Called when the application starts.
    /// </summary>
    protected override void OnInit()
    {
        base.OnInit();

        // Custom event handler executed after the appointment is created
        AppointmentInfo.TYPEINFO.Events.Insert.After += AppointmentEvents.Insert_After;
    }
}

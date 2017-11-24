using System;

using DoctorAppointments;

public partial class AddDoctor : System.Web.UI.Page
{
    protected void btnAddDoctor_Click(object sender, EventArgs e)
    {
        // Create new DoctorInfo object
        var doctor = new DoctorInfo();
        doctor.DoctorFirstName = "John";
        doctor.DoctorLastName = "Smith";
        doctor.DoctorCodeName = "JohnSmith";
        doctor.DoctorEmail = "JSmith@hospital.com";
        doctor.DoctorSpecialty = "Family Medicine";
        doctor.DoctorLastModified = DateTime.Now;

        // Insert doctor
        DoctorInfoProvider.SetDoctorInfo(doctor);

        Response.Write(doctor.DoctorFirstName + " " + doctor.DoctorLastName + " was added.");
    }
}
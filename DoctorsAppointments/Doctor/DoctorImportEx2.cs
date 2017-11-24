using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using CMS.Base;
using CMS.Scheduler;
using CMS.EventLog;
using CMS.Helpers;

/// <summary>
/// Scheduled task for importing list of doctors from source .csv file provided by path.
/// </summary>
namespace DoctorAppointments
{
    public class DoctorImportEx2 : ITask
    {
        public string Execute(TaskInfo ti)
        {
            string result = "";

            try
            {
                // Load path from TaskData property
                string filePath = FileHelper.GetFullFilePhysicalPath(ti.TaskData.Trim());

                if (File.Exists(filePath))
                {
                    using (var context = new CMSActionContext())
                    {
                        // Disable logging of all system operations like event log, web farm task etc. to improve performance.
                        context.DisableAll();
                        // Disable logging of staging tasks
                        // context.LogSynchronization = false;

                        // Read all lines
                        var lines = File.ReadAllLines(filePath);

                        var doctors = new List<DoctorInfo>();

                        // Loop through each line and get individual fields: column0 = FisrtName, column1 = LastName, column2 = Email, column3 = Number, column4 = Speciality
                        foreach (var line in lines)
                        {
                            var fields = line.Split(',');

                            // Create new doctor
                            var doctor = new DoctorInfo(){
                                DoctorFirstName = fields[0],
                                DoctorLastName = fields[1],
                                DoctorEmail = fields[2],
                                DoctorCodeName = fields[3],
                                DoctorSpecialty = fields[4]
                            };

                            // Add doctor to a list
                            doctors.Add(doctor);
                        }

                        // Set all doctors
                        int inserted = DoctorInfoProvider.SetDoctors(doctors);

                        // Doctors were successfully imported
                        result = string.Format("{0} new doctor(s) was/were imported.", inserted);
                    }
                }
                else
                {
                    // Prepare error message.
                    result = string.Format("File '{0}' does not exist.", filePath);
                }
            }
            catch (Exception e)
            {
                result = e.Message;
            }

            // Logs the execution of the task in the event log.
            EventLogProvider.LogInformation("DoctorsAppointment", "IMPORT", result);

            // Return result of scheduled task execution.
            return result;
        }
    }
}
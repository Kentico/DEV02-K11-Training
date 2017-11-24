using System;

using CMS;
using CMS.MacroEngine;
using CMS.Helpers;

/// <summary>
/// Summary description for DoctorAppointmentsMacroMethods
/// </summary>
// Makes all methods in the 'DoctorAppointmentsMacroMethods' container class available for objects
[assembly: RegisterExtension(typeof(DoctorAppointmentsMacroMethods), typeof(object))]
public class DoctorAppointmentsMacroMethods : MacroMethodContainer
{
    [MacroMethod(typeof(int), "Returns person age based on birthdate.", 1)]
    [MacroMethodParam(0, "Birthdate", typeof(DateTime), "Date of birth, for example '06/22/1985'")]
    public static object GetAge(EvaluationContext context, params object[] parameters)
    {
        // Branches according to the number of the method's parameters
        switch (parameters.Length)
        {
            case 1:
                // Overload with one parameter
                DateTime birth = ValidationHelper.GetDate(parameters[0], DateTime.Now);
                DateTime today = DateTime.Today;

                int age = today.Year - birth.Year;

                if (today.Month < birth.Month ||
                   ((today.Month == birth.Month) && (today.Day < birth.Day)))
                {
                    //birthday in current year not yet reached, we are 1 year younger ;)
                    //+ no birthday for 29.2. guys ... sorry, just wrong date for birth
                    age--;  
                }

                return age;

            default:
                // No other overloads are supported
                throw new NotSupportedException();
        }
    }
}
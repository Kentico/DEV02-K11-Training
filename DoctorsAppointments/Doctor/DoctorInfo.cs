using System;
using System.Data;
using System.Runtime.Serialization;

using CMS;
using CMS.DataEngine;
using CMS.Helpers;

using DoctorAppointments;

[assembly: RegisterObjectType(typeof(DoctorInfo), DoctorInfo.OBJECT_TYPE)]
    
namespace DoctorAppointments
{
    /// <summary>
    /// DoctorInfo data container class.
    /// </summary>
	[Serializable]
    public class DoctorInfo : AbstractInfo<DoctorInfo>
    {
        #region "Type information"

        /// <summary>
        /// Object type
        /// </summary>
        public const string OBJECT_TYPE = "doctorappointments.doctor";


        /// <summary>
        /// Type information.
        /// </summary>
        public static ObjectTypeInfo TYPEINFO = new ObjectTypeInfo(typeof(DoctorInfoProvider), OBJECT_TYPE, "DoctorAppointments.Doctor", "DoctorID", "DoctorLastModified", "DoctorGuid", "DoctorCodeName", "DoctorLastName", null, null, null, null)
        {
			ModuleName = "DoctorAppointments",
			TouchCacheDependencies = true,
            		LogEvents = true,
        };

        #endregion


        #region "Properties"

        /// <summary>
        /// Doctor ID
        /// </summary>
        [DatabaseField]
        public virtual int DoctorID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("DoctorID"), 0);
            }
            set
            {
                SetValue("DoctorID", value);
            }
        }


        /// <summary>
        /// Doctor guid
        /// </summary>
        [DatabaseField]
        public virtual Guid DoctorGuid
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("DoctorGuid"), Guid.Empty);
            }
            set
            {
                SetValue("DoctorGuid", value);
            }
        }


        /// <summary>
        /// Doctor last modified
        /// </summary>
        [DatabaseField]
        public virtual DateTime DoctorLastModified
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("DoctorLastModified"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("DoctorLastModified", value);
            }
        }


        /// <summary>
        /// Doctor code name
        /// </summary>
        [DatabaseField]
        public virtual string DoctorCodeName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DoctorCodeName"), String.Empty);
            }
            set
            {
                SetValue("DoctorCodeName", value, String.Empty);
            }
        }


        /// <summary>
        /// Doctor first name
        /// </summary>
        [DatabaseField]
        public virtual string DoctorFirstName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DoctorFirstName"), String.Empty);
            }
            set
            {
                SetValue("DoctorFirstName", value);
            }
        }


        /// <summary>
        /// Doctor last name
        /// </summary>
        [DatabaseField]
        public virtual string DoctorLastName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DoctorLastName"), String.Empty);
            }
            set
            {
                SetValue("DoctorLastName", value);
            }
        }


        /// <summary>
        /// Doctor specialty
        /// </summary>
        [DatabaseField]
        public virtual string DoctorSpecialty
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DoctorSpecialty"), String.Empty);
            }
            set
            {
                SetValue("DoctorSpecialty", value, String.Empty);
            }
        }


        /// <summary>
        /// Doctor email
        /// </summary>
        [DatabaseField]
        public virtual string DoctorEmail
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DoctorEmail"), String.Empty);
            }
            set
            {
                SetValue("DoctorEmail", value);
            }
        }

        #endregion


        #region "Type based properties and methods"

        /// <summary>
        /// Deletes the object using appropriate provider.
        /// </summary>
        protected override void DeleteObject()
        {
            DoctorInfoProvider.DeleteDoctorInfo(this);
        }


        /// <summary>
        /// Updates the object using appropriate provider.
        /// </summary>
        protected override void SetObject()
        {
            DoctorInfoProvider.SetDoctorInfo(this);
        }

        #endregion


        #region "Constructors"

		/// <summary>
        /// Constructor for de-serialization.
        /// </summary>
        /// <param name="info">Serialization info</param>
        /// <param name="context">Streaming context</param>
        public DoctorInfo(SerializationInfo info, StreamingContext context)
            : base(info, context, TYPEINFO)
        {
        }


        /// <summary>
        /// Constructor - Creates an empty DoctorInfo object.
        /// </summary>
        public DoctorInfo()
            : base(TYPEINFO)
        {
        }


        /// <summary>
        /// Constructor - Creates a new DoctorInfo object from the given DataRow.
        /// </summary>
        /// <param name="dr">DataRow with the object data</param>
        public DoctorInfo(DataRow dr)
            : base(TYPEINFO, dr)
        {
        }

        #endregion
    }
}

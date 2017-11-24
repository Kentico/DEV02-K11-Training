using System;

using CMS.DataEngine;

namespace DoctorAppointments
{    
    /// <summary>
    /// Class providing AppointmentInfo management.
    /// </summary>
    public class AppointmentInfoProvider : AbstractInfoProvider<AppointmentInfo, AppointmentInfoProvider>
    {
        #region "Constructors"

        /// <summary>
        /// Constructor
        /// </summary>
        public AppointmentInfoProvider()
            : base(AppointmentInfo.TYPEINFO)
        {
        }

        #endregion


        #region "Public methods - Basic"

        /// <summary>
        /// Returns a query for all the AppointmentInfo objects.
        /// </summary>
        public static ObjectQuery<AppointmentInfo> GetAppointments()
        {
            return ProviderObject.GetAppointmentsInternal();
        }


        /// <summary>
        /// Returns AppointmentInfo with specified ID.
        /// </summary>
        /// <param name="id">AppointmentInfo ID</param>
        public static AppointmentInfo GetAppointmentInfo(int id)
        {
            return ProviderObject.GetAppointmentInfoInternal(id);
        }


        /// <summary>
        /// Returns AppointmentInfo with specified name.
        /// </summary>
        /// <param name="name">AppointmentInfo name</param>
        public static AppointmentInfo GetAppointmentInfo(string name)
        {
            return ProviderObject.GetAppointmentInfoInternal(name);
        }


        /// <summary>
        /// Returns AppointmentInfo with specified GUID.
        /// </summary>
        /// <param name="guid">AppointmentInfo GUID</param>                
        public static AppointmentInfo GetAppointmentInfo(Guid guid)
        {
            return ProviderObject.GetAppointmentInfoInternal(guid);
        }


        /// <summary>
        /// Sets (updates or inserts) specified AppointmentInfo.
        /// </summary>
        /// <param name="infoObj">AppointmentInfo to be set</param>
        public static void SetAppointmentInfo(AppointmentInfo infoObj)
        {
            ProviderObject.SetAppointmentInfoInternal(infoObj);
        }


        /// <summary>
        /// Deletes specified AppointmentInfo.
        /// </summary>
        /// <param name="infoObj">AppointmentInfo to be deleted</param>
        public static void DeleteAppointmentInfo(AppointmentInfo infoObj)
        {
            ProviderObject.DeleteAppointmentInfoInternal(infoObj);
        }


        /// <summary>
        /// Deletes AppointmentInfo with specified ID.
        /// </summary>
        /// <param name="id">AppointmentInfo ID</param>
        public static void DeleteAppointmentInfo(int id)
        {
            AppointmentInfo infoObj = GetAppointmentInfo(id);
            DeleteAppointmentInfo(infoObj);
        }

        #endregion


        #region "Internal methods - Basic"
	
        /// <summary>
        /// Returns a query for all the AppointmentInfo objects.
        /// </summary>
        protected virtual ObjectQuery<AppointmentInfo> GetAppointmentsInternal()
        {
            return GetObjectQuery();
        }    


        /// <summary>
        /// Returns AppointmentInfo with specified ID.
        /// </summary>
        /// <param name="id">AppointmentInfo ID</param>        
        protected virtual AppointmentInfo GetAppointmentInfoInternal(int id)
        {	
            return GetInfoById(id);
        }


        /// <summary>
        /// Returns AppointmentInfo with specified name.
        /// </summary>
        /// <param name="name">AppointmentInfo name</param>        
        protected virtual AppointmentInfo GetAppointmentInfoInternal(string name)
        {
            return GetInfoByCodeName(name);
        } 


        /// <summary>
        /// Returns AppointmentInfo with specified GUID.
        /// </summary>
        /// <param name="guid">AppointmentInfo GUID</param>
        protected virtual AppointmentInfo GetAppointmentInfoInternal(Guid guid)
        {
            return GetInfoByGuid(guid);
        }


        /// <summary>
        /// Sets (updates or inserts) specified AppointmentInfo.
        /// </summary>
        /// <param name="infoObj">AppointmentInfo to be set</param>        
        protected virtual void SetAppointmentInfoInternal(AppointmentInfo infoObj)
        {
            SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified AppointmentInfo.
        /// </summary>
        /// <param name="infoObj">AppointmentInfo to be deleted</param>        
        protected virtual void DeleteAppointmentInfoInternal(AppointmentInfo infoObj)
        {
            DeleteInfo(infoObj);
        }	

        #endregion
    }
}
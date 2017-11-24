using System;
using System.Collections.Generic;

using CMS.DataEngine;

namespace DoctorAppointments
{    
    /// <summary>
    /// Class providing DoctorInfo management.
    /// </summary>
    public class DoctorInfoProvider : AbstractInfoProvider<DoctorInfo, DoctorInfoProvider>
    {
        #region "Constructors"

        /// <summary>
        /// Constructor
        /// </summary>
        public DoctorInfoProvider()
            : base(DoctorInfo.TYPEINFO)
        {
        }

        #endregion


        #region "Public methods - Basic"

        /// <summary>
        /// Returns a query for all the DoctorInfo objects.
        /// </summary>
        public static ObjectQuery<DoctorInfo> GetDoctors()
        {
            return ProviderObject.GetDoctorsInternal();
        }


        /// <summary>
        /// Returns DoctorInfo with specified ID.
        /// </summary>
        /// <param name="id">DoctorInfo ID</param>
        public static DoctorInfo GetDoctorInfo(int id)
        {
            return ProviderObject.GetDoctorInfoInternal(id);
        }


        /// <summary>
        /// Returns DoctorInfo with specified name.
        /// </summary>
        /// <param name="name">DoctorInfo name</param>
        public static DoctorInfo GetDoctorInfo(string name)
        {
            return ProviderObject.GetDoctorInfoInternal(name);
        }


        /// <summary>
        /// Returns DoctorInfo with specified GUID.
        /// </summary>
        /// <param name="guid">DoctorInfo GUID</param>                
        public static DoctorInfo GetDoctorInfo(Guid guid)
        {
            return ProviderObject.GetDoctorInfoInternal(guid);
        }


        /// <summary>
        /// Sets (updates or inserts) specified DoctorInfo.
        /// </summary>
        /// <param name="infoObj">DoctorInfo to be set</param>
        public static void SetDoctorInfo(DoctorInfo infoObj)
        {
            ProviderObject.SetDoctorInfoInternal(infoObj);
        }


        /// <summary>
        /// Deletes specified DoctorInfo.
        /// </summary>
        /// <param name="infoObj">DoctorInfo to be deleted</param>
        public static void DeleteDoctorInfo(DoctorInfo infoObj)
        {
            ProviderObject.DeleteDoctorInfoInternal(infoObj);
        }


        /// <summary>
        /// Deletes DoctorInfo with specified ID.
        /// </summary>
        /// <param name="id">DoctorInfo ID</param>
        public static void DeleteDoctorInfo(int id)
        {
            DoctorInfo infoObj = GetDoctorInfo(id);
            DeleteDoctorInfo(infoObj);
        }


        /// <summary>
        /// Inserts / Updates list of DoctorInfos.
        /// </summary>
        /// <param name="doctors">List of DoctorInfos</param>
        public static int SetDoctors(List<DoctorInfo> doctors)
        {
            int count = 0;

            foreach (DoctorInfo doctor in doctors )
            {
                DoctorInfo doc =  DoctorInfoProvider.GetDoctorInfo(doctor.DoctorCodeName);

                if(doc == null)
                {
                    DoctorInfoProvider.SetDoctorInfo(doctor);
                }
                else
                {
                    doc.DoctorFirstName = doctor.DoctorFirstName;
                    doc.DoctorLastName = doctor.DoctorLastName;
                    doc.DoctorEmail = doctor.DoctorEmail;
                    doc.DoctorLastModified = DateTime.Now;
                    DoctorInfoProvider.SetDoctorInfo(doc);
                    count++;
                }
            }

            return count;
        }

        #endregion


        #region "Internal methods - Basic"

        /// <summary>
        /// Returns a query for all the DoctorInfo objects.
        /// </summary>
        protected virtual ObjectQuery<DoctorInfo> GetDoctorsInternal()
        {
            return GetObjectQuery();
        }    


        /// <summary>
        /// Returns DoctorInfo with specified ID.
        /// </summary>
        /// <param name="id">DoctorInfo ID</param>        
        protected virtual DoctorInfo GetDoctorInfoInternal(int id)
        {	
            return GetInfoById(id);
        }


        /// <summary>
        /// Returns DoctorInfo with specified name.
        /// </summary>
        /// <param name="name">DoctorInfo name</param>        
        protected virtual DoctorInfo GetDoctorInfoInternal(string name)
        {
            return GetInfoByCodeName(name);
        } 


        /// <summary>
        /// Returns DoctorInfo with specified GUID.
        /// </summary>
        /// <param name="guid">DoctorInfo GUID</param>
        protected virtual DoctorInfo GetDoctorInfoInternal(Guid guid)
        {
            return GetInfoByGuid(guid);
        }


        /// <summary>
        /// Sets (updates or inserts) specified DoctorInfo.
        /// </summary>
        /// <param name="infoObj">DoctorInfo to be set</param>        
        protected virtual void SetDoctorInfoInternal(DoctorInfo infoObj)
        {
            SetInfo(infoObj);
        }


        /// <summary>
        /// Deletes specified DoctorInfo.
        /// </summary>
        /// <param name="infoObj">DoctorInfo to be deleted</param>        
        protected virtual void DeleteDoctorInfoInternal(DoctorInfo infoObj)
        {
            DeleteInfo(infoObj);
        }	

        #endregion
    }
}
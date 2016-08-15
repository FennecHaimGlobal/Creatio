using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatioUsersData
{
    public class UserDataManagment : IUserDataManagment
    {
        #region Members

        /// <summary>
        /// The _instance
        /// </summary>
        private static IUserDataManagment _instance;

        /// <summary>
        /// The _locker
        /// </summary>
        private static object _locker = new object();

        private string _connectionString = string.Empty;

        #endregion

        #region Properties
        /// <summary>
        /// Gets the get instance.
        /// </summary>
        /// <value>
        /// The get instance.
        /// </value>
        public static IUserDataManagment GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new UserDataManagment();
                        }
                    }
                }
                return _instance;
            }
        }

        public string ConnectionString
        {
            get
            {
                if (string.IsNullOrEmpty(_connectionString))
                {
                    if (ConfigurationManager.ConnectionStrings["user-connection-provider"] != null)
                    {
                        _connectionString = ConfigurationManager.ConnectionStrings["user-connection-provider"].ToString();
                    }

                }

                return _connectionString;
            }
        }

        public async Task AddUserToRole(string userId, string roleId)
        {
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
            {
                //AspNetUserRole userRole = new AspNetUserRole()
                //{
                //    UserId = userId,
                //    RoleId = roleId
                //};

                //context.AspNetUserRoles.Add(userRole);

                //await context.SaveChangesAsync();
            }
        }

        public async Task<string> GetRoleIdByRoleName(string roleName)
        {
            string result = default(string);
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
            {
                //var response = context.AspNetRoles.Where(T => T.Name == roleName).SingleOrDefault();

                //if (response != null)
                //{
                //    result = response.Id;
                //}
            }
            return result;
        }
        #endregion

        public async Task SaveAvocatsInformation(Avocat avocats)
        {
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
            {
                context.Avocats.Add(avocats);

                await context.SaveChangesAsync();
            }
        }

        public async Task SaveCommercialInformation(Commercial commercials)
        {
            using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
            {
                context.Commercials.Add(commercials);

                await context.SaveChangesAsync();
            }
        }

        public async Task SaveUserInformation(Information informations)
        {
            try
            {
                using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
                {
                    context.Informations.Add(informations);

                    await context.SaveChangesAsync();
                }
            }
            catch (TypeLoadException ex)
            {

                throw ex;
            }
            catch (TypeInitializationException ex1)
            {

                throw ex1;
            }
            catch (Exception ex3)
            {

                throw ex3;
            }
        }

        public async Task SaveCallback(Callback callback)
        {
            
            try 
	        {	        
		        using (CREATIO_DB_PRODEntities context = new CREATIO_DB_PRODEntities(ConnectionString))
                {
                    var alreadyExistingCallbacks = context.Callback
                        .Where(t => t.Telephone.Equals(callback.Telephone) && t.CallbackOk == 0);
                    if (await alreadyExistingCallbacks.CountAsync() == 0)
                    {
                        context.Callback.Add(callback);
                    }
                    else
                    {
                        // (current entry) 18:20  -> (last existing entry)18:00 Not OK, 18:10 Not OK, 17:59 OK
                        DateTime currentDate = DateTime.Now.AddMinutes(-20); 
                        var repeatCallback = await alreadyExistingCallbacks.FirstOrDefaultAsync(t => t.Date.CompareTo(currentDate) < 0);
                        if (repeatCallback != null)
                        {
                            repeatCallback.Repeat += 1;
                            repeatCallback.Date = callback.Date;
                        }
                        else
                        {
                            throw new ManyRequestsException();
                        }
                    }
                    context.SaveChanges();
                }
	        }
	        catch (Exception ex)
	        {
		        throw ex; 
	        }
        }
    }
    public partial class CREATIO_DB_PRODEntities : DbContext
    {
        public CREATIO_DB_PRODEntities(string connectionString)
            : base(connectionString)
        {
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using FiyiRequirements.Areas.BasicCore.Models;
using FiyiRequirements.Areas.CMSCore.Filters;
using FiyiRequirements.Areas.CMSCore.Protocols;
using FiyiRequirements.Areas.CMSCore.Models;
using FiyiRequirements.Library;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Text;
using System.IO;
using SixLaborsCaptcha.Core;

/*
 * GUID:e6c09dfe-3a3e-461b-b3f9-734aee05fc7b
 * 
 * Coded by fiyistack.com
 * Copyright © 2022
 * 
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 * 
 */

//Last modification on: 21/12/2022 11:12:12

namespace FiyiRequirements.Areas.CMSCore.Controllers
{
    /// <summary>
    /// Stack:             6<br/>
    /// Name:              C# Web API Controller. <br/>
    /// Function:          Allow you to intercept HTPP calls and comunicate with his C# Service using dependency injection.<br/>
    /// Last modification: 21/12/2022 11:12:12
    /// </summary>
    [ApiController]
    [UserFilter]
    public partial class UserValuesController : ControllerBase
    {
        private readonly IWebHostEnvironment _WebHostEnvironment;
        private readonly UserProtocol _UserProtocol;

        public UserValuesController(IWebHostEnvironment WebHostEnvironment, UserProtocol UserProtocol)
        {
            _WebHostEnvironment = WebHostEnvironment;
            _UserProtocol = UserProtocol;
        }

        #region Queries
        [HttpGet("~/api/CMSCore/User/1/Select1ByUserIdToJSON/{UserId:int}")]
        public UserModel Select1ByUserIdToJSON(int UserId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                return _UserProtocol.Select1ByUserIdToModel(UserId);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return null;
            }
        }

        [HttpGet("~/api/CMSCore/User/1/SelectAllToJSON")]
        public List<UserModel> SelectAllToJSON()
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                return _UserProtocol.SelectAllToList();
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return null;
            }
        }

        [HttpPut("~/api/CMSCore/User/1/SelectAllPagedToJSON")]
        public userModelQuery SelectAllPagedToJSON([FromBody] userModelQuery userModelQuery)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                return _UserProtocol.SelectAllPagedToModel(userModelQuery);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return null;
            }
        }
        #endregion

        #region Non-Queries
        [HttpPost("~/api/CMSCore/User/1/InsertOrUpdateAsync")]
        [Produces("text/plain")]
        public async Task<IActionResult> InsertOrUpdateAsync()
        {
            try
            {

                //Add or edit value
                string AddOrEdit = HttpContext.Request.Form["cmscore-user-title-page"];

                string FantasyName = HttpContext.Request.Form["cmscore-user-fantasyname-input"];
                string Email = HttpContext.Request.Form["cmscore-user-email-input"];
                string Password = "";
                if (HttpContext.Request.Form["cmscore-user-password-input"] != "")
                {
                    Password = Security.EncodeString(HttpContext.Request.Form["cmscore-user-password-input"]);
                }
                else
                {
                    return StatusCode(200, "Please, enter a password");
                }
                int RoleId = 0;
                if (Convert.ToInt32(HttpContext.Request.Form["cmscore-user-roleid-input"]) != 0)
                {
                    RoleId = Convert.ToInt32(HttpContext.Request.Form["cmscore-user-roleid-input"]);
                }
                else
                { return StatusCode(400, "It's not allowed to save zero values in RoleId"); }
                string RegistrationToken = HttpContext.Request.Form["cmscore-user-registrationtoken-input"];


                int NewEnteredId = 0;
                int RowsAffected = 0;

                if (AddOrEdit.StartsWith("Add"))
                {
                    //Add
                    UserModel UserModel = new UserModel()
                    {
                        Active = true,
                        UserCreationId = Convert.ToInt32(HttpContext.Request.Form["cmscore-user-userid-input"]),
                        UserLastModificationId = Convert.ToInt32(HttpContext.Request.Form["cmscore-user-userid-input"]),
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        FantasyName = FantasyName,
                        Email = Email,
                        Password = Password,
                        RoleId = RoleId,
                        RegistrationToken = RegistrationToken,

                    };

                    NewEnteredId = _UserProtocol.Insert(UserModel);
                }
                else
                {
                    //Update
                    int UserId = Convert.ToInt32(HttpContext.Request.Form["cmscore-user-userid-input"]);
                    UserModel UserModel = new UserModel(UserId);

                    UserModel.UserLastModificationId = UserId;
                    UserModel.DateTimeLastModification = DateTime.Now;
                    UserModel.FantasyName = FantasyName;
                    UserModel.Email = Email;
                    UserModel.Password = Password;
                    UserModel.RoleId = RoleId;
                    UserModel.RegistrationToken = RegistrationToken;


                    RowsAffected = _UserProtocol.UpdateByUserId(UserModel);
                }


                //Look for sent files
                if (HttpContext.Request.Form.Files.Count != 0)
                {
                    int i = 0; //Used to iterate in HttpContext.Request.Form.Files
                    foreach (var File in Request.Form.Files)
                    {
                        if (File.Length > 0)
                        {
                            var FileName = HttpContext.Request.Form.Files[i].FileName;
                            var FilePath = $@"{_WebHostEnvironment.WebRootPath}/Uploads/CMSCore/User/";

                            using (var FileStream = new FileStream($@"{FilePath}{FileName}", FileMode.Create))
                            {

                                await File.CopyToAsync(FileStream); // Read file to stream
                                byte[] array = new byte[FileStream.Length]; // Stream to byte array
                                FileStream.Seek(0, SeekOrigin.Begin);
                                FileStream.Read(array, 0, array.Length);
                            }

                            i += 1;
                        }
                    }
                }

                if (AddOrEdit.StartsWith("Add"))
                {
                    return StatusCode(200, NewEnteredId);
                }
                else
                {
                    return StatusCode(200, RowsAffected);
                }
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("~/api/CMSCore/User/1/DeleteByUserId/{UserId:int}")]
        [Produces("text/plain")]
        public IActionResult DeleteByUserId(int UserId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                int RowsAffected = _UserProtocol.DeleteByUserId(UserId);
                return StatusCode(200, RowsAffected);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/DeleteManyOrAll/{DeleteType}")]
        [Produces("text/plain")]
        public IActionResult DeleteManyOrAll([FromBody] Ajax Ajax, string DeleteType)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                _UserProtocol.DeleteManyOrAll(Ajax, DeleteType);

                return StatusCode(200, Ajax.AjaxForString);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/CopyByUserId/{UserId:int}")]
        [Produces("text/plain")]
        public IActionResult CopyByUserId(int UserId)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                int NewEnteredId = _UserProtocol.CopyByUserId(UserId);

                return StatusCode(200, NewEnteredId);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/CopyManyOrAll/{CopyType}")]
        [Produces("text/plain")]
        public IActionResult CopyManyOrAll([FromBody] Ajax Ajax, string CopyType)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                int[] NewEnteredIds = _UserProtocol.CopyManyOrAll(Ajax, CopyType);
                string NewEnteredIdsAsString = "";

                for (int i = 0; i < NewEnteredIds.Length; i++)
                {
                    NewEnteredIdsAsString += NewEnteredIds[i].ToString() + ",";
                }
                NewEnteredIdsAsString = NewEnteredIdsAsString.TrimEnd(',');

                return StatusCode(200, NewEnteredIdsAsString);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/Login")]
        [Produces("text/plain")]
        public IActionResult Login()
        {
            try
            {
                string FantasyNameOrEmail = HttpContext.Request.Form["fantasynameoremail"];
                string Password = HttpContext.Request.Form["password"];

                UserModel UserModel = _UserProtocol.Login(FantasyNameOrEmail, Password);

                if (UserModel.UserId != 0)
                {
                    HttpContext.Session.SetInt32("UserId", UserModel.UserId);
                    return StatusCode(200, "/CMSCore/DashboardIndex");
                }
                else
                {
                    return StatusCode(200, "User not found");
                }
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("~/api/CMSCore/User/1/ChangePassword")]
        [Produces("text/plain")]
        public IActionResult ChangePassword()
        {
            try
            {
                int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
                string ActualPassword = HttpContext.Request.Form["actual-password"];
                string NewPassword = HttpContext.Request.Form["new-password"];

                string Message = _UserProtocol.ChangePassword(UserId, ActualPassword, NewPassword);

                return StatusCode(200, Message);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost("~/api/CMSCore/User/1/Register")]
        [Produces("text/plain")]
        public IActionResult Register()
        {
            try
            {
                string FantasyName = HttpContext.Request.Form["fantasy-name"];
                string Email = HttpContext.Request.Form["email"];
                string Password = HttpContext.Request.Form["password"];
                string CaptchaText = HttpContext.Request.Form["captcha-text"];

                string CaptchaTextInSession = HttpContext.Session.GetString("CaptchaKey");

                if (CaptchaText != CaptchaTextInSession)
                {
                    return StatusCode(200, "The captcha is invalid");
                }

                string Message = _UserProtocol.Register(FantasyName, Email, Password);

                return StatusCode(200, Message);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("~/api/CMSCore/User/1/RecoverPassword")]
        [Produces("text/plain")]
        public IActionResult RecoverPassword()
        {
            try
            {
                string Email = HttpContext.Request.Form["email"];

                string Message = _UserProtocol.RecoverPassword(Email);

                return StatusCode(200, Message);
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("~/api/CMSCore/User/1/Logout")]
        [Produces("text/plain")]
        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Clear();
                return StatusCode(200, "/Index");
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }

        }
        #endregion

        #region Other actions
        [HttpPost("~/api/CMSCore/User/1/ExportAsPDF/{ExportationType}")]
        [Produces("text/plain")]
        public IActionResult ExportAsPDF([FromBody] Ajax Ajax, string ExportationType)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                DateTime Now = _UserProtocol.ExportAsPDF(Ajax, ExportationType);

                return StatusCode(200, new Ajax() { AjaxForString = Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") });
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/ExportAsExcel/{ExportationType}")]
        [Produces("text/plain")]
        public IActionResult ExportAsExcel([FromBody] Ajax Ajax, string ExportationType)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                DateTime Now = _UserProtocol.ExportAsExcel(Ajax, ExportationType);

                return StatusCode(200, new Ajax() { AjaxForString = Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") });
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/CMSCore/User/1/ExportAsCSV/{ExportationType}")]
        [Produces("text/plain")]
        public IActionResult ExportAsCSV([FromBody] Ajax Ajax, string ExportationType)
        {
            try
            {
                var SyncIO = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (SyncIO != null) { SyncIO.AllowSynchronousIO = true; }

                DateTime Now = _UserProtocol.ExportAsCSV(Ajax, ExportationType);

                return StatusCode(200, new Ajax() { AjaxForString = Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") });
            }
            catch (Exception ex)
            {
                DateTime Now = DateTime.Now;
                FailureModel FailureModel = new FailureModel()
                {
                    HTTPCode = 500,
                    Message = ex.Message,
                    EmergencyLevel = 1,
                    StackTrace = ex.StackTrace ?? "",
                    Source = ex.Source ?? "",
                    Comment = "",
                    Active = true,
                    UserCreationId = 1,
                    UserLastModificationId = 1,
                    DateTimeCreation = Now,
                    DateTimeLastModification = Now
                };
                FailureModel.Insert();
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("~/api/CMSCore/User/1/GetCaptchaImage")]
        public FileResult GetCaptchaImage([FromServices] ISixLaborsCaptchaModule sixLaborsCaptcha)
        {
            string CaptchaKey = Extensions.GetUniqueKey(6);
            var Image = sixLaborsCaptcha.Generate(CaptchaKey);

            HttpContext.Session.SetString("CaptchaKey", CaptchaKey);

            return File(Image, "Image/Png");
        }
        #endregion
    }
}
using FileUpload_ASP_MVC.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Web;
//using System.Web.;

namespace FileUploadApp.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        // POST: FileUpload
        [HttpPost]
        public ActionResult Upload(FileUploadViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null && model.File.ContentLength > 0)
                {
                    // Get the file extension
                    var fileExtension = Path.GetExtension(model.File.FileName).ToLower();

                    // Set allowed file types
                    var allowedFileTypes = new[] { ".jpg", ".jpeg", ".png", ".gif", ".mp3", ".mp4", ".pdf" };

                    // Check if the file is of the allowed type
                    if (Array.Exists(allowedFileTypes, ext => ext == fileExtension))
                    {
                        try
                        {
                            // Create a folder to store files (if not exists)
                            var path = Server.MapPath("~/UploadedFiles/");
                            if (!Directory.Exists(path))
                            {
                                Directory.CreateDirectory(path);
                            }

                            // Save the file
                            var filePath = Path.Combine(path, Path.GetFileName(model.File.FileName));
                            model.File.SaveAs(filePath);

                            ViewBag.Message = "File uploaded successfully!";
                        }
                        catch (Exception ex)
                        {
                            ViewBag.Message = $"File upload failed: {ex.Message}";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Invalid file type. Please upload an image, audio, video, or PDF file.";
                    }
                }
                else
                {
                    ViewBag.Message = "Please select a file to upload.";
                }
            }

            return View("Index");
        }
    }
}


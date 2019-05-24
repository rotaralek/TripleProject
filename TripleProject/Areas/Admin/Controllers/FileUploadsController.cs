using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TripleProject.Areas.Admin.Models;
using TripleProject.Areas.Admin.ViewModels;
using TripleProject.Data;

namespace TripleProject.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireAdministratorRole")]
    [Area("Admin")]
    public class FileUploadsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;

        public FileUploadsController(ApplicationDbContext context, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Admin/FileUploads
        public async Task<IActionResult> Index(int page = 1)
        {
            int itemsPerPage = 10;
            int skip = itemsPerPage * (page - 1);
            int count = await _context.FileUploads.CountAsync();
            var applicationDbContext = await _context.FileUploads.Skip(skip).Take(itemsPerPage).ToListAsync();

            ViewData["count"] = count;
            ViewData["page"] = page;
            ViewData["itemsPerPage"] = itemsPerPage;

            return View(applicationDbContext);
        }

        // GET: Admin/FileUploads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // GET: Admin/FileUploads/Create
        public IActionResult Create()
        {
            FileUploadViewModel ItemUpload = new FileUploadViewModel();

            return View(ItemUpload);
        }

        // POST: Admin/FileUploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemUpload")] IFormFileCollection ItemUpload)
        {
            if (ModelState.IsValid)
            {
                foreach (var uploadedFile in ItemUpload)
                {
                    string path = "/files/" + uploadedFile.FileName;

                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream);
                    }

                    FileUpload file = new FileUpload { Name = uploadedFile.FileName, Path = path };
                    _context.FileUploads.Add(file);
                }
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Admin/FileUploads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads.FindAsync(id);
            if (fileUpload == null)
            {
                return NotFound();
            }
            return View(fileUpload);
        }

        // POST: Admin/FileUploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Path,ItemUpload")] FileUpload fileUpload)
        {
            if (id != fileUpload.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileUpload);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileUploadExists(fileUpload.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fileUpload);
        }

        // GET: Admin/FileUploads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fileUpload = await _context.FileUploads
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fileUpload == null)
            {
                return NotFound();
            }

            return View(fileUpload);
        }

        // POST: Admin/FileUploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fileUpload = await _context.FileUploads.FindAsync(id);
            _context.FileUploads.Remove(fileUpload);
            await _context.SaveChangesAsync();

            string fileLocation = _appEnvironment.WebRootPath + fileUpload.Path;
            var bytes = System.IO.File.ReadAllBytes(fileLocation);
            System.IO.File.Delete(fileLocation);

            return RedirectToAction(nameof(Index));
        }

        private bool FileUploadExists(int id)
        {
            return _context.FileUploads.Any(e => e.Id == id);
        }

        [Route("AjaxUpload")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AjaxUpload(List<IFormFile> files)
        {
            List<Object> images = new List<Object>();
            int filesCounter = 1;
            int lastFileId = 0;

            foreach (IFormFile file in files)
            {
                FileUpload filesCount = _context.FileUploads.LastOrDefault<FileUpload>();

                if (filesCount != null)
                {
                    lastFileId = filesCount.Id;
                }

                int newIntFileName = lastFileId + filesCounter;
                string newFileName = Convert.ToString(newIntFileName);

                string fileName = file.FileName;
                string path = "/temporary-files/" + fileName;
                string newPath = "/files/" + newFileName + Path.GetExtension(fileName);

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                System.IO.File.Copy(_appEnvironment.WebRootPath + path, _appEnvironment.WebRootPath + newPath, true);
                System.IO.File.Delete(_appEnvironment.WebRootPath + path);

                FileUpload image = new FileUpload { Name = newFileName, Path = newPath };
                _context.FileUploads.Add(image);
                images.Add(image);

                filesCounter++;
            }

            _context.SaveChanges();

            return new JsonResult(images);
        }
    }
}

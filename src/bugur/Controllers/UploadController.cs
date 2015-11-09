namespace bugur.Controllers
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;
	using Microsoft.AspNet.Http;
	using Microsoft.AspNet.Mvc;
	using Microsoft.Dnx.Runtime;
	using Microsoft.Net.Http.Headers;

	public class UploadDto
	{
		public IFormFile Payload { get; set; }
	}

	[Route("api/[controller]")]
	public class UploadController : Controller
	{
		private static readonly Random Random = new Random();
		private readonly IApplicationEnvironment _appEnvironment;
		private readonly string _basePath;

		public UploadController(IApplicationEnvironment appEnvironment)
		{
			_appEnvironment = appEnvironment;
			_basePath = _appEnvironment.ApplicationBasePath;
		}

		public static string GetRandomString(int length)
		{
			const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
			return new string(Enumerable
				.Repeat(chars, length)
				.Select(s => s[Random.Next(s.Length)])
				.ToArray());
		}

		[HttpPost]
		public async Task<IActionResult> Post(UploadDto dto)
		{
			IFormFile file = dto.Payload;
			JsonResult result;
			try
			{
				// Implementation taken from here: http://dotnetthoughts.net/file-upload-in-asp-net-5-and-mvc-6/
				string fileName = ContentDispositionHeaderValue
					.Parse(file.ContentDisposition)
					.FileName
					.Trim('"')
					.ToLowerInvariant();

				if (Regex.IsMatch(fileName, @"(\.png|\.jpg|\.gif)$"))
				{
					string fullName = GetRandomString(8) + Path.GetExtension(fileName);
					string filePath = Path.Combine(_basePath, "wwwroot", "images", fullName);
					await file.SaveAsAsync(filePath);
					// See http://plugins.krajee.com/file-input#ajax-uploads
					string imageUrl = $"/images/{fullName}";
					string markup = $"<img src='{imageUrl}' class='file-preview-image' alt='{fullName}' title='{fullName}' /> " +
						$"<p><a href='{imageUrl}'>Link</a></p>";
					return base.Created(
						imageUrl,
						new
						{
							initialPreview = new[] { markup },
							initialPreviewConfig = new object[] { new { caption = fullName } },
							imageUrl = imageUrl
						});
				}
				else
				{
					result = Json(new { error = "Invalid file type." });
					result.StatusCode = StatusCodes.Status400BadRequest;
				}
			}
			catch (Exception ex)
			{
				result = Json(new { error = "Server error. " + ex });
				result.StatusCode = StatusCodes.Status500InternalServerError;
			}
			return result;
		}

	}
}
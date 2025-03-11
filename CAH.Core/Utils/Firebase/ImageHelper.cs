using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CAH.Core.Utils.Firebase
{
	public class ImageHelper
	{
        private readonly string _bucket;

        public ImageHelper(IConfiguration configuration)
		{
            _bucket = configuration["Firebase:Bucket"];
            if (FirebaseApp.DefaultInstance == null)
            {
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile("collegeadmissionhelper-firebase-adminsdk-fbsvc-7898fc81a5.json")
                });
            }
        }

		public async Task<string> Upload(IFormFile file)
		{
            if (file == null || file.Length == 0)
                return string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                return await UploadToFirebase(memoryStream, file.FileName);
            }
        }


        private async Task<string> UploadToFirebase(Stream stream, string fileName)
        {
            var storage = new FirebaseStorage(_bucket);
            var storageRef = storage.Child("images").Child(fileName);

            await storageRef.PutAsync(stream);
            return await storageRef.GetDownloadUrlAsync();
        }
    }
}

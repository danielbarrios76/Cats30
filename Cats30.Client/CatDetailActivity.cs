using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using System.Net;

namespace Cats30.Client
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/Icon")]
    public class CatDetailActivity : Activity
    {
        //para almacenar los valores pasados de la Activity Main
        string id;
        string name;
        string description;
        string website;
        string imageURL;
        int price;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CatDetail);

            //recuperamos los valores
            id = Intent.Extras.GetString("Id");
            name = Intent.Extras.GetString("Name");
            description = Intent.Extras.GetString("Description");
            website = Intent.Extras.GetString("WebSite");
            imageURL = Intent.Extras.GetString("ImageURL");
            price = Intent.Extras.GetInt("Price");

            //Cambiamos el titulo del Layout
            this.Title = name;


            var imageView = FindViewById<ImageView>(Resource.Id.imageViewCatDetail);
            imageView.SetImageBitmap(GetImageBitmapFromUrl(imageURL));
            //Koush.UrlImageViewHelper.SetUrlDrawable(imageView, image);

            FindViewById<TextView>(Resource.Id.textViewCatName).Text = name;
            FindViewById<TextView>(Resource.Id.textViewCatDescription).Text = description;

            FindViewById<Button>(Resource.Id.buttonVisitWebsite).Click += (s, e) =>
            {
                var uri = Android.Net.Uri.Parse(website);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };
        }

        //Metodo privado para cargar una imagen desde una URL
        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}
using Android.App;
using Android.Widget;
using Android.OS;
using Cats30.SAL;
using Cats30.CustomAdapters;
using Android.Content;

namespace Cats30.Client
{
    [Activity(Label = "@string/ApplicationName", Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            var service = new ServiceClient();
            var catsList = await service.GetCats();

            var listviewCats = FindViewById<ListView>
                (Resource.Id.listviewCats);
            listviewCats.Adapter = new CustomAdapter
                (this, catsList, Resource.Layout.ListItem, 
                Resource.Id.imageViewCat, 
                Resource.Id.textViewName, 
                Resource.Id.textViewPrice);

            listviewCats.ItemClick += (s, e) =>
            {
                var cat = catsList[e.Position];

                var intent = new Intent(this, typeof(CatDetailActivity));
                intent.PutExtra("Id", cat.ID);
                intent.PutExtra("Name", cat.Name);
                intent.PutExtra("Description", cat.Description);
                intent.PutExtra("Price", cat.Price);
                intent.PutExtra("ImageURL", cat.Image);
                intent.PutExtra("WebSite", cat.WebSite);
                StartActivity(intent);
            };
        }
    }
}


using Android.App;
using Android.OS;
using Android.Runtime;
using AndroidX.AppCompat.App;
using Android.Graphics;
using Android.Widget;

namespace GoodFood_Projeto
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState) 
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            TextView textView = FindViewById<TextView>(Resource.Id.txt_1);
            
            //Só assim é que se consegue mudar a linha do texto e colocando as propriedades da fonte como AndroidAsset
            Typeface typeface = Typeface.CreateFromAsset(Assets, "PlaypenSans.ttf");
            textView.SetTypeface(typeface, TypefaceStyle.Normal);

            ImageView imageView = FindViewById<ImageView>(Resource.Id.main_imageView);
            Button button = FindViewById<Button>(Resource.Id.main_button);

            button.Click += (sender, e) =>
            {
                // Vai para a segunda ATIVIDADE
                StartActivity(typeof(SegundaPagina)); 
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
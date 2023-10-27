using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace GoodFood_Projeto
{
    [Activity(Label = "SegundaPagina",MainLauncher =false, Theme = "@style/AppTheme")]
    public class SegundaPagina : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.SegundaPagina);

            TextView textView1 = FindViewById<TextView>(Resource.Id.txt_2);

            Typeface typeface1 = Typeface.CreateFromAsset(Assets, "PlaypenSans.ttf");
            textView1.SetTypeface(typeface1, TypefaceStyle.Normal);

            Button procurar = FindViewById<Button>(Resource.Id.btn_procurar);
            Button inserir = FindViewById<Button>(Resource.Id.btn_inserir);
            Button alterar = FindViewById<Button>(Resource.Id.btn_alterar);
            Button apagar = FindViewById<Button>(Resource.Id.btn_apagar);

            procurar.Click += Procurar_Click;
            inserir.Click += Inserir_Click;
            alterar.Click += Alterar_Click;
            apagar.Click += Apagar_Click;
        }

        private void Procurar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(procurar_receita));
        }
        private void Inserir_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(inserir_receita));
        }
        private void Alterar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(alterar_receita));
        }
        private void Apagar_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(apagar_receita));
        }
    }
}
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SQLite;


namespace GoodFood_Projeto
{
    [Activity(Label = "procurar_receita")]
    public class procurar_receita : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.procurar_receita);
            // Create your application here

            Button voltar = FindViewById<Button>(Resource.Id.btn_voltar);

            voltar.Click += Voltar_Click;
        }

        private void Voltar_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(SegundaPagina)));
        }
    }
    
}
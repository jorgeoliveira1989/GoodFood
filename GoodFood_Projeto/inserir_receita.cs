﻿using Android.App;
using Android.Content;
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
    [Activity(Label = "inserir_receita")]
    public class inserir_receita : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.inserir_receita);
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
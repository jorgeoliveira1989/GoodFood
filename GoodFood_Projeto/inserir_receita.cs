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
using static Android.Provider.ContactsContract;

namespace GoodFood_Projeto
{
    [Activity(Label = "inserir_receita")]
    public class inserir_receita : Activity
    {
        EditText et_id;
        EditText et_receita;
        EditText et_categoria;
        EditText et_modo_preparo;

        string dbPath =
            System.IO.Path.Combine(
              System.Environment.GetFolderPath(
                  System.Environment.SpecialFolder.Personal),
              "receitas.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.inserir_receita);
            // Create your application here

            et_id = FindViewById<EditText>(Resource.Id.et_id);
            et_receita = FindViewById<EditText>(Resource.Id.et_receita);
            et_categoria = FindViewById<EditText>(Resource.Id.et_categoria);
            et_modo_preparo = FindViewById<EditText>(Resource.Id.et_modo_preparo);
            Button inserir = FindViewById<Button>(Resource.Id.btn_inserir);
            inserir.Click += Inserir_Click;

            Button voltar = FindViewById<Button>(Resource.Id.btn_voltar);
            voltar.Click += Voltar_Click;
        }

        private void Inserir_Click(object sender, EventArgs e)
        {
            var db = new SQLite.SQLiteConnection(dbPath);

            db.CreateTable<receita>();
            var table = db.Table<receita>();

            int numReceitas = 0;
            int maximo = 0;

            foreach (var item in table)
            {

                if (item.id_receita > maximo)
                    maximo = (int)item.id_receita;
                
                numReceitas++;
            }

            if (maximo > numReceitas)
                numReceitas = maximo;
            numReceitas++;

            receita minhareceita = new receita(numReceitas,et_receita.Text,et_categoria.Text,et_modo_preparo.Text);

            if (et_receita.Text !="" && et_categoria.Text !="" && et_modo_preparo.Text !="")
            {
                db.Insert(minhareceita);

                Toast.MakeText(this, "Receita" + numReceitas.ToString() + "Inserido com Sucesso!", ToastLength.Short).Show();
            }

            db.Close();

            limpa_entrada();

        }

        private void Voltar_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(SegundaPagina)));
        }

        private void limpa_entrada()
        {
            et_id.Text = "";
            et_receita.Text = "";
            et_categoria.Text = "";
            et_modo_preparo.Text = "";
        }
    }
}
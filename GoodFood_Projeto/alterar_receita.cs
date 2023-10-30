using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Javax.Security.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodFood_Projeto
{
    [Activity(Label = "alterar_receita")]
    public class alterar_receita : Activity
    {

        Spinner spinner_id;
        EditText et_receita;
        EditText et_categoria;
        EditText et_modo_preparo;
        JavaList<string> ListaIDs = new JavaList<string>();
        long idSelecionado = -1;

        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "receitas.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.alterar_receita);
            // Create your application here

            et_receita = FindViewById<EditText>(Resource.Id.et_receita);
            et_categoria = FindViewById<EditText>(Resource.Id.et_categoria);
            et_modo_preparo = FindViewById<EditText>(Resource.Id.et_modo_preparo);

            todasReceitas();

            Button alterar = FindViewById<Button>(Resource.Id.btn_alterar);

            alterar.Click += Alterar_Click;

            Button voltar = FindViewById<Button>(Resource.Id.btn_voltar);

            voltar.Click += Voltar_Click;
        }

        private void Alterar_Click(object sender, EventArgs e)
        {
            bool encontraReceita = false;

            var db = new SQLite.SQLiteConnection(dbPath);

            try
            {
                var table = db.Table<receita>();
                long id = spinner_id.SelectedItemId;


                foreach (var item in table)
                {
                    if (id == item.id_receita)
                    {
                        receita minhaReceita = new receita(item.id_receita, et_receita.Text, et_categoria.Text, et_modo_preparo.Text);

                        if (et_receita.Text != "" && et_categoria.Text != "" && et_modo_preparo.Text != "")
                        {
                            //item.id_receita = spinner_id.SelectedItemId;
                            item.nome_receita = et_receita.Text ;
                            item.categoria = et_categoria.Text ;
                            item.descricao_receita = et_modo_preparo.Text;
                            db.Update(item);
                            limparEcra();

                            Toast.MakeText(this, "Receita " + item.id_receita.ToString() + " atualizado com sucesso!", ToastLength.Short).Show();
                            encontraReceita = true;
                        }
                    }
                }

                db.Close();

                if (!encontraReceita)
                {
                    Toast.MakeText(this, "Receita não encontrada!", ToastLength.Short).Show();
                }
            }
            catch (System.Exception ex)
            {

            }
        }
            private void Voltar_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(SegundaPagina)));
        }
        private void limparEcra()
        {
                            et_receita.Text = "";
                            et_categoria.Text = "";
                            et_modo_preparo.Text = "";
        }
        private void todasReceitas()
        {
            var db = new SQLite.SQLiteConnection(dbPath);
            try
            {
                var table = db.Table<receita>();

                foreach (var item in table)
                {

                    ListaIDs.Add(item.id_receita.ToString());
                }

                spinner_id = FindViewById<Spinner>(Resource.Id.spinner_id);

                spinner_id.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, ListaIDs);

                spinner_id.ItemSelected += spinner_id_ItemSelected;

            }
            catch (System.Exception ex)
            {

            }
        }
        private void spinner_id_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            bool encontraReceita = false;

            var db = new SQLite.SQLiteConnection(dbPath);

            try
            {
                var table = db.Table<receita>();

                long id = long.Parse(ListaIDs[e.Position]);

                foreach (var item in table)
                {
                    if (id == item.id_receita)
                    {
                        et_receita.Text = item.nome_receita;
                        et_categoria.Text = item.categoria;
                        et_modo_preparo.Text = item.descricao_receita;

                        Toast.MakeText(this, "Receita " + item.id_receita.ToString() + " encontrada com sucesso!", ToastLength.Short).Show();
                        encontraReceita = true;
                    }
                }

                if (!encontraReceita)
                {
                    Toast.MakeText(this, "Receita não encontrada!", ToastLength.Long).Show();
                }
            }
            catch (System.Exception ex)
            {

            }
        }
    }
}
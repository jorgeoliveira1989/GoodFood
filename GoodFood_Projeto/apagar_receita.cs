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
using System.Threading.Tasks;
using static Android.Content.ClipData;

namespace GoodFood_Projeto
{
    [Activity(Label = "apagar_receita")]
    public class apagar_receita : Activity
    {

        Spinner spinner_id;
        EditText et_receita;
        EditText et_categoria;
        EditText et_modo_preparo;
        JavaList<string> ListaIDs = new JavaList<string>();
        

        string dbPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "receitas.db3");

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.apagar_receita);
            // Create your application here

            et_receita = FindViewById<EditText>(Resource.Id.et_receita);
            et_categoria = FindViewById<EditText>(Resource.Id.et_categoria);
            et_modo_preparo = FindViewById<EditText>(Resource.Id.et_modo_preparo);

            todasReceitas();

            Button apagar = FindViewById<Button>(Resource.Id.btn_apagar);

            apagar.Click += Apagar_Click;

            Button voltar = FindViewById<Button>(Resource.Id.btn_voltar);

            voltar.Click += Voltar_Click;
        }

        private async void Apagar_Click(object sender, EventArgs e)
        {
            bool encontraReceita = false;
            bool escolheuSim = false;

            var db = new SQLite.SQLiteConnection(dbPath);

            try
            {
                var table = db.Table<receita>();

                int posicaoSelecionada = spinner_id.SelectedItemPosition; // Obtem a posição selecionada

                if (posicaoSelecionada >= 0 && posicaoSelecionada < ListaIDs.Count) // Verifica se a posição é válida
                {
                    long id = long.Parse(ListaIDs[posicaoSelecionada]); // Obtém o ID da receita

                    string mensagem = "Tem a certeza que quer eliminar a Receita ";
                    string titulo = "Confirmação da Eliminação";

                    foreach (var item in table)
                    {
                        if (id == item.id_receita)
                        {
                            encontraReceita = true;
                            mensagem += item.id_receita + " ?";
                            escolheuSim = await AlertAsync(this, titulo, mensagem, "Sim", "Não");

                            if (escolheuSim)
                            {
                                db.Delete(item);

                                Toast.MakeText(this, "Receita " + item.id_receita.ToString() + " eliminada com sucesso!", ToastLength.Short).Show();
                                escolheuSim = false;
                                limparEcra();
                                break;
                            }
                            else
                            {

                                Toast.MakeText(this, "Operação " + item.id_receita.ToString() + " cancelada!", ToastLength.Short).Show();
                                limparEcra();
                                break;
                            }

                        }
                    }

                    db.Close();

                    if (!encontraReceita)
                    {
                        Toast.MakeText(this, "Receita não encontrada!", ToastLength.Short).Show();
                    }
                }
            }
            catch (System.Exception ex)
            {
                // Trate exceções se necessário
            }
        }
            private void Voltar_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(this, typeof(SegundaPagina)));
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

        private void limparEcra()
        {
           spinner_id.SetSelection(-1);
           et_receita.Text = "";
           et_categoria.Text = "";
           et_modo_preparo.Text = "";
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

        public Task<bool> AlertAsync(Context context,
            string title, string message,
            string positiveButton,
            string negativeButton)
        {
            // Cria objecto de task
            var tcs = new TaskCompletionSource<bool>();


            // Obtém e apresenta 
            // objeto de Alerta - Dialog Box
            using (var db = new Android.App
                .AlertDialog.Builder(context))
            {
                db.SetTitle(title);
                db.SetMessage(message);

                // Define resultado
                db.SetPositiveButton(positiveButton,
                    (sender, args) => {
                        tcs.TrySetResult(true);
                    });
                db.SetNegativeButton(negativeButton,
                    (sender, args) => {
                        tcs.TrySetResult(false);
                    });

                // Apresenta Dialog Box
                db.Show();
            }

            // Devolve booleano
            return tcs.Task;
        } // Fim da Função "AlertAsync()".

    }
}
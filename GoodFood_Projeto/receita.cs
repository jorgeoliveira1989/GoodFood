using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoodFood_Projeto
{
    class receita
    {
        [SQLite.PrimaryKey]
        public long id_receita { get; set; }

        public string nome_receita { get; set; }

        public string categoria { get; set; }

        public string descricao_receita { get; set; }

        public receita()
        {


        }

        public receita(long id_rec, string nome_rec, string cat, string desc_cat)
        {
            id_receita = id_rec;
            nome_receita = nome_rec;
            categoria = cat;
            descricao_receita = desc_cat;
        }

        public receita(string nome_rec, string cat, string desc_cat)
        {
            nome_receita = nome_rec;
            categoria = cat;
            descricao_receita = desc_cat;
        }

        public override string ToString()
        {
            return "id receita: " + id_receita.ToString() + "\n" + "nome receita: " + nome_receita + "\n" + "categoria: " + categoria + "\n" + "Descrição receita" + descricao_receita + "\n";
        }
    }
    
}
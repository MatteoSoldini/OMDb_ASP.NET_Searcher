using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMdb_Film_Searcher
{
    public partial class Form1 : Form
    {
        string TOKEN = "6b939174";
        HttpClient client = new HttpClient();
        SearchResult SearchResult;

        async Task<SearchResult> getSearchAsync(string path)
        {
            HttpResponseMessage response;
            try
            {
                response = await client.GetAsync(path);
                if (response.IsSuccessStatusCode)
                {
                    SearchResult = await response.Content.ReadAsAsync<SearchResult>();
                    listBox1.Items.Clear();
                    foreach (var movie in SearchResult.Search)
                    {
                        listBox1.Items.Add(movie.Title);
                    }
                    //pictureBox1.ImageLocation = search.Poster;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Nope");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
            }
            return SearchResult;
        }

        async Task searchFilmRequest(string title)
        {
            client.BaseAddress = new Uri("https://www.omdbapi.com/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            try
            {

                SearchResult = await getSearchAsync("?apikey="+ TOKEN +"&s=" + title);

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message.ToString());
            }
        }
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await searchFilmRequest(textBox1.Text);
            client = new HttpClient();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            pictureBox1.ImageLocation = SearchResult.Search[i].Poster;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}

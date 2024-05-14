using SQLite;
using System.Collections.Specialized;

namespace Sites;

public partial class MainPage : ContentPage
{
	string dbPatch; // Indica onde esta o banco de dados
	SQLiteConnection conn; // Representa a conexao do banco de dados 



	public MainPage()
	{
		InitializeComponent();
        
    }

    public void ListarSites()
    {
        List<Site> lista = conn.Table<Site>().ToList();
        ListaClv.ItemsSource = lista;
       //CollectionView
    }

    private void criarBanco_Clicked(object sender, EventArgs e)
    {
		dbPatch = System.IO.Path.Combine(FileSystem.AppDataDirectory, "sites.db3");
		conn = new SQLiteConnection(dbPatch);
		conn.CreateTable<Site>();
        itensVsl.IsVisible= true;
        ListarSites();
    }
    private void inserirEnt_Clicked(object sender, EventArgs e)
    {
        //pegar os dados da tela
        // 1 int id = Convert.ToInt32(idEnt.Text);
        // 2 string endereco = siteEnt.Text;
        Site site = new Site();
        site.Endereco = siteEnt.Text;

        //realizar as operações
        try 
        {
            conn.Insert(site);
            //Limpar dados
            siteEnt.Text = "";
            idEnt.Text = "";
            // Caixa de texto para confirmar
            DisplayAlert("Cadastrar", "Site Cadastrado com Sucesso!!!", "Ok");
        }

        catch
        {
            // Caixa de texto para confirmar
            DisplayAlert("Erro", "Site ja foi Cadastrado!!!", "Ok");
        }
        //exibir uma resposta do resultado da atividade
        ListarSites();
    }

    private void alterarEnt_Clicked(object sender, EventArgs e)
    {
        Site site = new Site();
        site.Id = Convert.ToInt32(idEnt.Text);
        site.Endereco = siteEnt.Text;

        conn.Update(site);

        //Limpar dados
        siteEnt.Text = "";
        idEnt.Text = "";
        // Caixa de texto para confirmar
        DisplayAlert("Alterado", "Site Alterado com Sucesso!!!", "Ok");
        ListarSites();
    }

    private void excluirEnt_Clicked(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(idEnt.Text);
        conn.Delete<Site>(id);
        //Limpar dados
        siteEnt.Text = "";
        idEnt.Text = "";
        // Caixa de texto para confirmar
        DisplayAlert("Deletar", "Site Deletado com Sucesso!!!", "Ok");
        ListarSites();
    }

    private void localizarEnt_Clicked(object sender, EventArgs e)
    {
        int id = Convert.ToInt32(idEnt.Text);
        string endereco = siteEnt.Text;

        var sites = from s in conn.Table<Site>()
                    where s.Id == id
                    select s;


        Site site = sites.First();
        idEnt.Text = site.Id.ToString();
        siteEnt.Text = site.Endereco;
        ListarSites();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label != null && label.BindingContext is Site item) 
        { 
            idEnt.Text = item.Id.ToString();
            siteEnt.Text = item.Endereco;
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
    {
        var label = sender as Label;
        if (label != null && label.BindingContext is Site item)
        {
            Launcher.OpenAsync(new Uri(item.Endereco));
            
        }

    }
}


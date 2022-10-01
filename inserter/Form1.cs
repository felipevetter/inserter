using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inserter
{
    public partial class Form1 : Form
    {
        public login lg;

        public Form1(login lge)
        {
            lg = lge;
            InitializeComponent();
        }
        public int maxTables = 0;

        public int generatedTables = 0;

        public bool unlimited = false;

        public List<string> Rules = new List<string>();

        public Guna.UI2.WinForms.Guna2Button generateButton(string txt)
        {
            Guna.UI2.WinForms.Guna2Button btn = new Guna.UI2.WinForms.Guna2Button();
            btn.HoverState.FillColor = Color.FromArgb(203, 98, 153);
            btn.HoverState.BorderColor = Color.FromArgb(203, 98, 153);
            btn.FillColor = Color.FromArgb(203, 98, 153);
            btn.Text = "New rule: " + txt;
            btn.BorderRadius = 12;
            return btn;
        }

        public void generateRule(string rule)
        {
            Rules.Add(rule);
            flowLayoutPanel1.Controls.Add(generateButton(rule));
        }

        public static Random rnd = new Random();

        public static class CpfUtils
        {  

            public static String GerarCpf()
            {
                int soma = 0, resto = 0;
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                
                string semente = rnd.Next(100000000, 999999999).ToString();

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                semente = semente + resto;
                soma = 0;

                for (int i = 0; i < 10; i++)
                    soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

                resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                semente = semente + resto;
                return semente;
            }
            public static string GerarRG()
            {
                int n1 = rnd.Next(0, 10);
                int n2 = rnd.Next(0, 10);
                int n3 = rnd.Next(0, 10);
                int n4 = rnd.Next(0, 10);
                int n5 = rnd.Next(0, 10);
                int n6 = rnd.Next(0, 10);
                int n7 = rnd.Next(0, 10);
                int n8 = rnd.Next(0, 10);

                int soma = (n1 * 2) + (n2 * 3) + (n3 * 4) + (n4 * 5) + (n5 * 6) + (n6 * 7) + (n7 * 8) + (n8 * 9);
                int resto = soma % 11;
                string DV;
                if(resto == 0)
                {
                    DV = "0";
                }
                else
                {
                    DV = (11 - resto).ToString();
                }
                return n1.ToString() + n2 + n3 + n4 + n5 + n6 + n7 + n8 + DV;
            }
        }

        public void closePanel(FontAwesome.Sharp.IconPictureBox iconPictureBox, Guna.UI2.WinForms.Guna2Panel panel)
        {
            iconPictureBox.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleRight;
            panel.Visible = false;
        }

        public void openPanel(FontAwesome.Sharp.IconPictureBox iconPictureBox, Guna.UI2.WinForms.Guna2Panel panel)
        {
            iconPictureBox.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleLeft;
            panel.Visible = true;
        }

        public int generateId()
        {
            return rnd.Next(10000, 99999);
        }

        public string getName()
        {
            string[] nomes = { "Emma", "Charlotte", "Amelia", "Ava", "Sophia", "Isabella", "Mia", "Evelyn", "Harper", "Luna", "Camila", "Gianna", "Elizabeth", "Eleanor", "Ella", "Abigail", "Sofia", "Avery", "Scarlett", "Emily", "Aria", "Liam", "Noah", "Oliver", "Elijah", "James", "William", "Benjamin", "Lucas", "Henry", "Theodore", "Jack", "Levi", "Alexander", "Jackson", "Mateo", "Daniel", "Michael", "Mason", "Sebastian", "Ethan", "Logan", "Owen", "Kai", "Zion", "Jayden", "Eliana", "Luca", "Ezra", "Maeve", "Aaliyah", "Mia", "Nova", "Aurora", "Amara", "Kayden", "Ivy", "Alina", "Mila", "Quinn", "Rowan", "Elliot", "Finn", "Lilibet", "River", "Xavier", "Rachel", "Amaya", "Remi", "Axel", "Zoey", "Malachi", "Alex", "Blake", "Lyla", "Alice", "Isla", "Rebecca", "Rohan", "Milo", "Elias", "Ari", "Aria", "Molly", "Jude", "Isabella", "Arthur", "Millie", "Andrea", "Marcus", "Atlas", "Ariella", "Kyle", "Evan", "Ira", "Hayden", "Bailey", "Gianna", "Valerie", "Brianna", "Jesse", "Cecilia", "Leo", "Leilani", "Dante", "Zoe", "Khadijah", "Mya", "Sharon", "Sean", "Brielle", "Ayla", "Shia", "Riley", "Raya", "Sloane", "Alana", "Charlie", "Kian", "Hudson", "Elise", "Akira", "Mika", "Freya", "Nia", "Natasha", "Myra", "Mateo", "Everett", "Rae", "Savannah", "Thea", "Finley", "Alaina", "Mina", "Yara", "Emerson", "Camille", "Ivan", "Skyler", "Skylar", "Alma", "Reese", "Sasha", "Asa", "Sage", "Camila", "Amira", "Kieran", "Monica", "Everly", "Evie", "Maverick", "Kyra", "Ian", "Julia", "Vivian", "Theo", "Ophelia", "Chelsea", "Azariah", "Jade", "Lara", "Ava", "Morgan", "Lennox", "Luna", "Isabelle", "Amir", "Rhys", "Arlo", "Giovanni", "Aisha", "Orion", "Ahmed", "Nolan", "Ezekiel", "Michelle", "Lea", "Silas", "Elaine", "Adira", "Callan", "Lilith", "Justin", "Simon", "Rhea", "Marie", "Lisa", "Damien", "Ximena", "Lilah", "Elora", "Sienna", "Fiona", "Urban", "Jean", "Eden", "Kayla", "Avi", "Octavia", "Skye", "Nancy", "Otis", "Lila", "Anya", "Elena", "Zayne", "Gigi", "Alyssa", "Amelia", "Giselle", "Francis", "Jacqueline", "Aiden", "Kylie", "Wren", "Maria", "Mae", "Colette", "Louise", "Aliyah", "Chase", "Tara", "Lorenzo", "Sydney", "Callie", "Niko", "Avery", "Gemma", "Rafael", "Hailey", "Harlow", "Adeline", "Margot", "Rory", "Nyla", "Helena", "Colin", "Xander", "Rylee", "Irene", "Ashton", "Marley", "Arden", "Kaira", "Arianna", "Pia", "Nola", "Miles", "Brooks", "Annalise", "Imani", "Josie", "Ellis", "Cali", "Hadassah", "Phoenix", "Piper", "Emery", "Aliza", "Mackenzie", "Timothy", "Adrian", "Sawyer", "Harvey", "Enoch", "Lachlan", "Kaiden", "Zuri", "Shelby", "Liam", "Olivia", "Aubrey", "Sanjana", "Rayne", "Stella", "Cleo", "Gracie", "Oakley", "Madeline", "Melissa", "Lauren", "Mona", "Alicia", "Jasmine", "Scott", "Abel", "Elliott", "Wesley", "Aditya", "Alan", "Brooke", "Sunny", "Sana", "Blair", "Leon", "Emmanuel", "Lilian", "Priya", "Malia", "Elodie", "Adriel", "Amos", "Trisha", "Naomi", "Damian", "Nevaeh", "Judah", "Sloan", "Joel", "Nicholas", "Azriel", "Lyra", "Lee", "Kevin", "Remy", "Omar", "Amelie", "Caleb", "Aleena", "Killian", "Theodore", "Asher", "Mariam", "Claudia", "Noelle", "Juliana", "Makayla", "Beau", "Nikita", "Beckett", "Nadia", "Ana", "Zane", "Jayce", "Brayden", "Elia", "Leia", "Cooper", "Zain", "Ronan", "Liana", "Kali", "Serena", "Davina", "Zaid", "Dillon", "Sylvia", "Rhiannon", "Ryder", "Zara", "Amina", "Keanu", "Amaris", "Eloise", "Mara", "Vera", "Sonny", "Keira", "Ali", "Sierra", "Harper", "Katherine", "Siobhan", "Ada", "Gia", "Heather", "Kalani", "Penny", "Camilla", "Cole", "Amani", "Emmet", "Leila", "Ethan", "Alani", "Fallon", "Joyce", "Joan", "Winifred", "Amyra", "Mira", "Quincy", "Kimberly", "Faye", "Colton", "Cayden", "Maira", "Ayana", "Shiloh", "Darren", "Evelyn", "Lorelei", "Iva", "Felix", "Tessa", "Jalen", "Rylan", "Nellie", "Masha", "Amora", "Alvin", "Leighton", "Keziah", "Mikayla", "Harley", "Oliver", "Huxley", "Martin", "Noa", "Rocco", "Shane", "Ines", "Rai", "Harry", "Lily", "Stanley", "Darcy", "Bryce", "Devin", "Lucas", "Jamie", "Teddy", "Martha", "Albert", "Travis", "Lucian", "Emelia", "Delilah", "Norah", "Azalea", "Valentina", "Hallie", "Nora", "Kara", "Misha", "Ishmael", "Mimi", "Pamela", "Genevieve", "Thalia", "Collin", "Grayson", "June", "May", "Kenji", "Chiara", "Ravi", "Rosie", "Seraphina", "Juno", "Sophie", "Simone", "Gavin", "Alayna", "Miriam", "Patricia", "Christine", "Joaquin", "Dior", "Israel", "Teagan", "Jocelyn", "Zaira", "Tiffany", "Cedric", "Reyna", "Winston", "Maximus", "Dennis", "George", "Braxton", "Deborah", "Lorraine", "Romy", "Dakota", "Reuben", "Hayley", "Anisha", "Saira", "Veda", "Tiana", "Kyler", "Preston", "Olive", "Ellie", "Rio", "Yvonne", "Parker", "Yana", "Maia", "Levi", "Tyson", "Graham", "Cain", "Kelvin", "Lynn", "Lia", "Kaden", "Rian", "Aurelia", "Spencer", "Usnavi", "Elina", "Ellen", "Kaya", "Tamara", "Mabel", "Remington", "Ember", "Sadie", "Sahil", "Azrael", "Kendall", "Raine", "Noah", "Athena", "Declan", "Leigh", "Helen", "Rey", "Janet", "Ace", "Alena", "Lola", "Karina", "Grace", "Jedidiah", "Alaia", "Aman", "Brian", "Milan", "Malcolm", "Javier", "Emma", "Marion", "Adaline", "Daisy", "Amal", "Holly", "Cillian", "Kayleigh" };
            
            int index = rnd.Next(nomes.Length);
            return nomes[index];
        }

        public string[] enderecos =
        {
            "2ª Travessa Elpídio de Almeida, Bairro Catolé, Campina Grande, Paraíba",
"Avenida Antônio Basílio, Bairro Lagoa Nova, Natal, Rio Grande do Norte",
"Avenida Açaí, Bairro Distrito Industrial I, Manaus, Amazonas",
"Avenida Bernardo Sayão, Bairro Vila Cearense, Araguaína, Tocantins",
"Avenida Copacabana, Bairro Conjunto Maria Rosa (Taquaralto), Palmas, Tocantins",
"Avenida do Trabalhador, Bairro Verolme, Angra dos Reis, Rio de Janeiro",
"Avenida Dom Antônio Soares Costa, Bairro José Carlos de Oliveira, Caruaru, Pernabumco",
"Avenida Macapá, Bairro Boné Azul, Macapá, Amapá",
"Avenida Marechal Rondon, Bairro Jardim Aeroporto, Bayeux, Paraíba",
"Avenida Nações Unidas, Bairro Bosque, Rio Branco, Acre",
"Avenida Sete de Setembro, Bairro Nova Porto Velho, Porto Velho, Rondônia",
"Avenida Terceira, Bairro Cabralzinho, Macapá, Amapá",
"Beco Jatobá, Bairro Colônia Antônio Aleixo, Manaus, Amazonas",
"Beco Tracuá, Bairro Tarumã-Açu, Manaus, Amazonas",
"Estrada da CODEAL, Bairro Tabuleiro do Martins, Maceió, Alagoas",
"Largo Eleonor Roosevelt, Bairro Coroa do Meio, Aracaju, Sergipe",
"Passeio das Flores, Bairro Fátima, Fortaleza, Santa Ceará",
"Praça Presidente Roosevelt, Bairro COHAB Anil III, São Luís, Maranhão",
"Quadra 305 Norte Avenida NS 3, Bairro Plano Diretor Norte, Palmas, Tocantins",
"Quadra AE 105 Sul Avenida Juscelino Kubitschek, Bairro Plano Diretor Sul, Palmas, Tocantins",
"Quadra Quadra 7 Comércio Local 9, Bairro Sobradinho, Brasília, Distrito Federal",
"Quadra Quadra 8 Bloco 15, Bairro Sobradinho, Brasília, Distrito Federal",
"Quadra SHN Quadra 2 Bloco N, Bairro Asa Norte, Brasília, Distrito Federal",
"Rua 09, Bairro Maracanã, Santarém, Pará",
"Rua 15-A, Bairro Parque 10 de Novembro, Manaus, Amazonas",
"Rua A, Bairro Aeroporto, Aracaju, Sergipe",
"Rua Afonso Barboza de Oliveira, Bairro Pedro Gondim, João Pessoa, Paraíba",
"Rua Alvorecer, Bairro Setor Morada do Sol, Goiânia, Goiás",
"Rua Aléssio Zermiani, Bairro Vila Nova, Joinville, Santa Catarina",
"Rua Argentina, Bairro Jardim Residencial Sabo, Santo Ângelo, Rio Grande do Sul",
"Rua Aziz Mutran, Bairro Nova Marabá, Marabá, Pará",
"Rua Belo Horizonte, Bairro Vila Lucy, Goiânia, Goiás",
"Rua Canadá, Bairro Itaum, Joinville, Santa Catarina",
"Rua Cinco de Janeiro, Bairro Ipiranga, Nova Iguaçu, Rio de Janeiro",
"Rua Corinto Trindade, Bairro João XXIII, Parnaíba, Piauí",
"Rua Coronel Manoel Benício, Bairro Castelo Branco, João Pessoa, Paraíba",
"Rua da Clorita, Bairro Lagoa Nova, Natal, Rio Grande do Norte",
"Rua do Coqueiro, Bairro Centro, São Luís, Maranhão",
"Rua dos Andradas, Bairro Nossa Senhora da Abadia, Uberaba, Minas Gerais",
"Rua Duque de Caxias 526, Bairro Centro, Inhuma, Piauí",
"Rua Euclides da Cunha, Bairro São José do Egito, Imperatriz, Maranhão",
"Rua Firmino Fontes, Bairro Atalaia, Aracaju, Sergipe",
"Rua Gabriel Ferreira, Bairro Aeroporto, Teresina, Piauí",
"Rua Guanabara, Bairro Niterói, Betim, Minas Gerais",
"Rua Guanambí, Bairro Açaí, Ji-Paraná, Rondônia",
"Rua Guilherme Ehrich de Menezes, Bairro Campo dos Velhos, Sobral, Santa Ceará",
"Rua Itabira, Bairro Niterói, Betim, Minas Gerais",
"Rua J s/n, Bairro Distrito de São Roque, São Roque, Santa Catarina",
"Rua José Nicolau de Carvalho, Bairro Santo Antônio de Pádua, Tubarão, Santa Catarina",
"Rua José Roberto de Souza, Bairro Velame, Campina Grande, Paraíba",
"Rua Manoel Santos Gaya, Bairro Centro, Navegantes, Santa Catarina",
"Rua Marfim, Bairro Mapim, Várzea Grande, Mato Grosso",
"Rua Marinete Lopes de Oliveira, Bairro São Luiz II, Arapiraca, Alagoas",
"Rua Monte Claro, Bairro Jutaí, Santarém, Pará",
"Rua Monte Sião, Bairro Residencial Rosa Linda, Rio Branco, Acre",
"Rua Nelson Albuquerque, Bairro Liberdade, Boa Vista, Estado RR",
"Rua Padre Marcos Anchieta, Bairro João de Deus, São Luís, Maranhão",
"Rua Poeta Bosco Lopes, Bairro Ponta Negra, Natal, Rio Grande do Norte",
"Rua Principal 66, Bairro Centro, Rosário de Minas, Minas Gerais",
"Rua Principal, s/n, Bairro Santa Clara, Cerro Corá, Rio Grande do Norte",
"Rua Projetada 898, Bairro Loteamento Joafra, Rio Branco, Acre",
"Rua Q, Bairro Passaré, Fortaleza, Santa Ceará",
"Rua Quarenta e Seis - A, Bairro Vinhais, São Luís, Maranhão",
"Rua Quinze -B, Bairro Varadouro, Olinda, Pernabumco",
"Rua Santa Clotilde, Bairro Santa Delmira, Mossoró, Rio Grande do Norte",
"Rua Santos Dumont, Bairro Centro, Crato, Santa Ceará",
"Rua Sepetiba, Bairro Jardim Espírito Santo, Uberaba, Minas Gerais",
"Rua São Mateus, Bairro São José, Macapá, Amapá",
"Rua Vereador Walmor Maes, Bairro Boehmerwald, Joinville, Santa Catarina",
"Rua Werner Duwe, Bairro Badenfurt, Blumenau, Santa Catarina",
"Rua Álvares de Azevedo, Bairro Compensa, Manaus, Amazonas",
"Travessa Braspetro, Bairro Stiep, Salvador, Bahía",
"Travessa Conceição Cabral, Bairro Roger, João Pessoa, Paraíba",
"Travessa Raimundo Preto, Bairro Jardim das Araras, Itaituba, Pará",
"Travessa Sn-11, Bairro Nova, Ananindeua, Pará",
"Vila João Gualberto, Bairro Prefeito José Walter, Fortaleza, Santa Ceará",
"Vila São Rafael, Bairro Maraponga, Fortaleza, Santa Ceará"
        };

        public string Rua()
        {
            int index = rnd.Next(enderecos.Length);
            var rua = enderecos[index].Split(',')[0];
            if(!rua.ToLower().StartsWith("rua"))
            {
                rua = "Rua " + rua;
            }
            return rua;
        }

        public string Bairro()
        {
            int index = rnd.Next(enderecos.Length);
            var bairro = enderecos[index].Split(',')[1];
            return bairro;
        }

        public string Cidade()
        {
            int index = rnd.Next(enderecos.Length);
            var cidade = enderecos[index].Split(',')[2];
            return cidade;
        }

        public string[] estados =
        {
            "Rio Branco",
"Alagoas",
"Amapá",
"Amazonas",
"Bahia",
"Ceara",
"Distrito Federal",
"Espírito Santo",
"Goiás",
"Maranhão",
"Mato Grosso",
"Mato Grosso do Sul",
"Minas Gerais",
"Pará",
"Paraíba",
"Paraná",
"Pernambuco",
"Piauí",
"Rio de Janeiro",
"Rio Grande do Norte",
"Rio Grande do Sul",
"Rondônia",
"Roraima",
"Santa Catarina",
"São Paulo",
"Sergipe",
"Tocantins"
        };
        public string Estado()
        {
            int index = rnd.Next(estados.Length);
            var cidade = estados[index];
            return cidade;
        }

        public string enderecoCompleto()
        {
            int index = rnd.Next(enderecos.Length);
            return enderecos[index];
        }

        public string saveWhere = "";

        public string randomDate()
        {
            int year;
            string month;
            string day;
            year = rnd.Next(DateTime.Now.Year - 20, DateTime.Now.Year + 20);
            month = rnd.Next(1, 12).ToString();
            day = rnd.Next(1, 31).ToString();

            if (int.Parse(month) < 10)
            {
                month = "0" + month;
            }
            if (int.Parse(day) < 10)
            {
                day = "0" + day;
            }

            return year + "-" + month + "-" + day;
        }

        public string initialDate()
        {
            int year;
            string month;
            string day;
            year = rnd.Next(DateTime.Now.Year - 20, DateTime.Now.Year);
            month = rnd.Next(1, 12).ToString();
            day = rnd.Next(1, 31).ToString();

            if (int.Parse(month) < 10)
            {
                month = "0" + month;
            }
            if (int.Parse(day) < 10)
            {
                day = "0" + day;
            }

            return year + "-" + month + "-" + day;
        }

        public string maxDate()
        {
            int year;
            string month;
            string day;
            year = rnd.Next(DateTime.Now.Year, DateTime.Now.Year + 15);
            month = rnd.Next(1, 12).ToString();
            day = rnd.Next(1, 31).ToString();
            if(int.Parse(month) < 10)
            {
                month = "0" + month;
            }
            if(int.Parse(day) < 10) 
            {
                day = "0" + day;
            }
            return year + "-" + month + "-" + day;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveWhere = openFileDialog1.FileName;
            }
        }

        private void iconPictureBox1_Click(object sender, EventArgs e)
        {
            if(guna2Panel1.Visible == false)
            {
                guna2Panel1.Visible = true;
                iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleLeft;
            } 
            else
            {
                iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.ArrowAltCircleRight;
                guna2Panel1.Visible = false;
            }
            
        }

        private void iconPictureBox2_Click(object sender, EventArgs e)
        {
            closePanel(iconPictureBox3, guna2Panel3);
            closePanel(iconPictureBox4, guna2Panel4);
            closePanel(iconPictureBox5, guna2Panel5);
            if (guna2Panel2.Visible == true)
            {
                closePanel(iconPictureBox2, guna2Panel2); 
            } else
            {
                openPanel(iconPictureBox2, guna2Panel2);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            generateRule("ID");
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            generateRule("CPF");
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            generateRule("NOME");
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            generateRule("NOME_SN");
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            generateRule("E_COMPLETO");
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            generateRule("RUA");
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            generateRule("BAIRRO");
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            generateRule("CIDADE");
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            generateRule("ESTADO");
        }

        public Dictionary<string, string> myRules = new Dictionary<string, string>();

        private string MakeCall(string loginData, string loginPass)
        {
            string url = "http://fvsolutions.herokuapp.com/getTables";

            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["email"] = lg.strVar;
                data["senha"] = lg.strVar2;
             
                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);
                return responseInString;
            }
        }

        private void SendTables(int tbs)
        {
            string url = "http://fvsolutions.herokuapp.com/sendTables";
          
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();
                data["email"] = lg.strVar;
                data["senha"] = lg.strVar2;
                data["howmany"] = tbs.ToString();

                var response = wb.UploadValues(url, "POST", data);
                string responseInString = Encoding.UTF8.GetString(response);
            }

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string loginData = lg.strVar;
            string loginPass = lg.strVar2;
            var resp = MakeCall(loginData, loginPass);
            if (resp != "unlimited")
            {
                maxTables = int.Parse(resp);
            }
            else
            {
                unlimited = true;
            }

            if (Rules.Count() == 0)
            {
                MessageBox.Show("Você precisa selecionar ao menos uma Rule!");
                return;
            }
            if(saveWhere == "")
            {
                MessageBox.Show("Selecione um lugar para salvar!");
                return;
            }
            if(testTextBox() == true)
            {
                return;
            }
            for(var i = 0; i < int.Parse(guna2TextBox2.Text); i++)
            {
                if (generatedTables >= maxTables)
                {
                    break;
                }
                var pattern = "insert into " + guna2TextBox1.Text + " \nvalues(";
                var result = pattern;
                for(var b = 0; b < Rules.Count(); b++)
                {
                    if (Rules[b] == "ID")
                    {
                        result += generateId();
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "CPF")
                    {
                        result += "\"" + CpfUtils.GerarCpf() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "NOME")
                    {
                        result += "\"" + getName() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "NOME_SN")
                    {
                        result += "\"" + getName() + " " + getName() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "E_COMPLETO")
                    {
                        result += "\"" + enderecoCompleto() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "RUA")
                    {
                        result += "\"" + Rua() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "BAIRRO")
                    {
                        result += "\"" + Bairro() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "CIDADE")
                    {
                        result += "\"" + Cidade() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "ESTADO")
                    {
                        result += "\"" + Estado() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }
                    
                    if (Rules[b].StartsWith("CUSTOM_TEXT"))
                    {
                        var index = Rules[b].Split('-')[1];
                        if (myRules.ElementAt(int.Parse(index) - 1).Value.Contains(";")) 
                        {
                            result += "\"" + myRules.ElementAt(int.Parse(index) - 1).Value.Split(';')[rnd.Next(guna2TextBox3.Text.Split(';').Length)] + "\"";
                            result = Regex.Replace(result, Regex.Escape("$d"), match => rnd.Next(0, 9).ToString());
                            result = Regex.Replace(result, Regex.Escape("$c"), match => ((char)rnd.Next(97, 122)).ToString());
                        }
                        else
                        {
                            result += "\"" + myRules.ElementAt(int.Parse(index) - 1).Value + "\"";
                            result = Regex.Replace(result, Regex.Escape("$d"), match => rnd.Next(0, 9).ToString());
                            result = Regex.Replace(result, Regex.Escape("$c"), match => ((char)rnd.Next(97, 122)).ToString());
                        }
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "R_DATE")
                    {
                        result += "'" + randomDate() + "'";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "I_DATE")
                    {
                        result += "'" + initialDate() + "'";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "F_DATE")
                    {
                        result += "'" + maxDate() + "'";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }

                    if (Rules[b] == "PLACA")
                    {
                        result += "\"" + gerarPlaca() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }
                    if (Rules[b] == "RG")
                    {
                        result += "\"" + CpfUtils.GerarRG() + "\"";
                        if ((b + 1) < Rules.Count())
                        {
                            result += ", ";
                        }
                    }
                }
                generatedTables++;
                result += ");";
                File.AppendAllText(saveWhere, result + "\n\n");
            }

            SendTables(generatedTables);
            generatedTables = 0;
        }

        public string generateNumber()
        {
            return rnd.Next(0, 9).ToString();
        }

        public string TFF = "";
        public bool testTextBox()
        {
            int i;
            if (!int.TryParse(guna2TextBox2.Text, out i))
            {
                guna2TextBox2.Text = "This is a number only field";
                return true;
            } 
            else
            {
                return false;
            }
        }

        private void iconPictureBox3_Click(object sender, EventArgs e)
        {
            closePanel(iconPictureBox2, guna2Panel2);
            closePanel(iconPictureBox4, guna2Panel4);
            closePanel(iconPictureBox5, guna2Panel5);
            if (guna2Panel3.Visible == true)
            {
                closePanel(iconPictureBox3, guna2Panel3);
            }
            else
            {
                openPanel(iconPictureBox3, guna2Panel3);
            }
        }

        private void guna2Button15_Click(object sender, EventArgs e)
        {
            myRules.Add(myRules.Count.ToString(), guna2TextBox3.Text);
            generateRule("CUSTOM_TEXT-" + myRules.Count().ToString());
        }

        private void guna2Button16_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Rules.Clear();

        }

        private void iconPictureBox4_Click(object sender, EventArgs e)
        {
            closePanel(iconPictureBox2, guna2Panel2);
            closePanel(iconPictureBox3, guna2Panel3);
            closePanel(iconPictureBox5, guna2Panel5);
            if (guna2Panel4.Visible == true)
            {
                closePanel(iconPictureBox4, guna2Panel4);
            }
            else
            {
                openPanel(iconPictureBox4, guna2Panel4);
            }
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            generateRule("R_DATE");
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            generateRule("I_DATE");
        }

        private void guna2Button20_Click(object sender, EventArgs e)
        {
            generateRule("F_DATE");
        }

        public string gerarPlaca()
        {
            string txtRand = string.Empty;
            for (int i = 0; i < 3; i++) txtRand += ((char)rnd.Next(97, 122)).ToString();
            return txtRand.ToUpper() + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9);
        }

        private void guna2Button21_Click(object sender, EventArgs e)
        {
            generateRule("PLACA");
        }

        private void iconPictureBox5_Click(object sender, EventArgs e)
        {
            closePanel(iconPictureBox2, guna2Panel2);
            closePanel(iconPictureBox3, guna2Panel3);
            closePanel(iconPictureBox4, guna2Panel4);
            if (guna2Panel5.Visible == true)
            {
                closePanel(iconPictureBox5, guna2Panel5);
            }
            else
            {
                openPanel(iconPictureBox5, guna2Panel5);
            }
        }

        private void guna2Button23_Click(object sender, EventArgs e)
        {
            generateRule("RG");
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            string loginData = lg.strVar;
            string loginPass = lg.strVar2;

            var resp = MakeCall(loginData, loginPass);
            if(resp != "unlimited")
            {
                maxTables = int.Parse(resp);
            } 
            else
            {
                unlimited = true;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace What_to_Watch
{
    public partial class Form1 : Form
    {
        // TODO: Add documentary folder and make sure it's working here
        // TODO: Add way to open all other programs here and there
        public Form1()
        {
            InitializeComponent();
            and_radio_tooltip.SetToolTip(and_radio, "For selecting a movie that is all of A + B + C genres");
            or_radio_tooltip.SetToolTip(or_radio, "For selecting a movie that is in A or B or C genres");
            MessageBox.Show("Please make sure the hard drive is connected under the E-drive. Then click 'OK' to proceed.", "Hard Drive Connection", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void exclusive_radio_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void inclusive_button_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void action_check_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void find_button_Click(object sender, EventArgs e)
        {
            string and_or = "";
            if (and_radio.Checked)
            {
                and_or = "and";
            }
            else if (or_radio.Checked)
            {
                and_or = "or";
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Please Select And/Or Operator", "And/Or", MessageBoxButtons.OK);
            }

            List<string> selected_cats = new List<string>();

            if (action_check.Checked)
            {
                selected_cats.Add("Action");
            }
            if (adventure_check.Checked)
            {
                selected_cats.Add("Adventure");
            }
            if (animated_check.Checked)
            {
                selected_cats.Add("Animated");
            }
            if (christmas_check.Checked)
            {
                selected_cats.Add("Christmas");
            }
            if (classic_check.Checked)
            {
                selected_cats.Add("Classics");
            }
            if (comedy_check.Checked)
            {
                selected_cats.Add("Comedy");
            }
            if (docu_check.Checked)
            {
                selected_cats.Add("Drama");
            }
            if (drama_check.Checked)
            {
                selected_cats.Add("Drama");
            }
            if (epic_check.Checked)
            {
                selected_cats.Add("Epic");
            }
            if (family_check.Checked)
            {
                selected_cats.Add("Family");
            }
            if (fantasy_check.Checked)
            {
                selected_cats.Add("Fantasy");
            }
            if (halloween_check.Checked)
            {
                selected_cats.Add("Halloween");
            }
            if (historical_check.Checked)
            {
                selected_cats.Add("Historical");
            }
            if (horror_check.Checked)
            {
                selected_cats.Add("Horror");
            }
            if (monster_check.Checked)
            {
                selected_cats.Add("Monster");
            }
            if (musical_check.Checked)
            {
                selected_cats.Add("Musical");
            }
            if (mystery_check.Checked)
            {
                selected_cats.Add("Mystery");
            }
            if (romance_check.Checked)
            {
                selected_cats.Add("Romance");
            }
            if (scifi_check.Checked)
            {
                selected_cats.Add("Science Fiction");
            }
            if (superhero_check.Checked)
            {
                selected_cats.Add("Superhero");
            }
            if (thriller_check.Checked)
            {
                selected_cats.Add("Thriller");
            }

            if (selected_cats.Count > 0)
            {
                if (and_or == "and")
                {
                    int count = 0;
                    Dictionary<string, string> last_dict = new Dictionary<string, string>();

                    foreach (string genre in selected_cats)
                    {
                        Dictionary<string, string> this_dict = new Dictionary<string, string>();

                        string filepath = System.IO.Path.Combine(@"E:\Media\Movies\", genre);

                        foreach (string movie_folder_path in System.IO.Directory.GetDirectories(filepath))
                        {
                            foreach (string movie_path in System.IO.Directory.GetFiles(System.IO.Path.Combine(filepath, movie_folder_path)))
                            {
                                string movie_name = System.IO.Path.GetFileNameWithoutExtension(movie_path);
                                movie_name = movie_name.Replace(" - Shortcut", "");
                                movie_name = movie_name.Replace(".mkv", "");
                                //movie_name = movie_name.Replace("1. ", "");
                                //movie_name = movie_name.Replace("2. ", "");
                                //movie_name = movie_name.Replace("3. ", "");
                                //movie_name = movie_name.Replace("4. ", "");
                                //movie_name = movie_name.Replace("5. ", "");
                                if (count == 0)
                                {
                                    //MessageBox.Show($"New movie: {movie_name}");
                                    this_dict.Add(movie_name, movie_path);
                                }
                                else
                                {
                                    if (last_dict.Keys.Contains(movie_name))
                                    {
                                        this_dict.Add(movie_name, movie_path);
                                    }
                                }
                            }
                        }

                        foreach (string movie_path in System.IO.Directory.GetFiles(filepath))
                        {
                            string movie_name = System.IO.Path.GetFileNameWithoutExtension(movie_path);
                            movie_name = movie_name.Replace(" - Shortcut", "");
                            movie_name = movie_name.Replace(".mkv", "");
                            //MessageBox.Show(movie_name);

                            if (count == 0)
                            {
                                //MessageBox.Show($"New movie: {movie_name}");
                                this_dict.Add(movie_name, movie_path);
                            }
                            else
                            {
                                if (last_dict.Keys.Contains(movie_name))
                                {
                                    this_dict.Add(movie_name, movie_path);
                                }
                            }
                        }
                        last_dict = this_dict;
                        count += 1;
                    }
                    if (last_dict.Keys.Count > 0)
                    {
                        randomizer_A:
                        int movie_dict_length = last_dict.Keys.Count;

                        DialogResult dialogResult = DialogResult.None;
                        string rnd_movie = "";

                        if (movie_dict_length > 0)
                        {
                            Random num = new Random();
                            int rnd_index = num.Next(movie_dict_length);
                            rnd_movie = last_dict.ElementAt(rnd_index).Key;

                            dialogResult = MessageBox.Show($"How about '{rnd_movie}'?", "Selected Movie", MessageBoxButtons.YesNoCancel);
                        }
                        else
                        {
                            MessageBox.Show("There are no movies left in this category. \nPlease rerun the selection or choose another.", "No More Movies", MessageBoxButtons.OK);
                        }
                        
                        if (dialogResult == DialogResult.Yes)
                        {
                        try
                        {
                            string path = last_dict[rnd_movie];
                            System.Diagnostics.Process.Start(path);
                        }
                        catch
                        {
                            try
                            {
                                string path = $@"E:\Media\Movies\All\{rnd_movie}.mp4";
                                System.Diagnostics.Process.Start(path);
                            }
                            catch
                            {
                                try
                                {
                                    string path = $@"E:\Media\Movies\All\{rnd_movie}\{rnd_movie}.mkv";
                                    System.Diagnostics.Process.Start(path);
                                }
                                catch
                                {
                                    try
                                    {
                                        string path = $@"E:\Media\Movies\All\{rnd_movie}\{rnd_movie}.mp4";
                                        System.Diagnostics.Process.Start(path);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Duology\{rnd_movie}.mkv";
                                            System.Diagnostics.Process.Start(path);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Trilogy\{rnd_movie}.mkv";
                                                System.Diagnostics.Process.Start(path);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Quadrology\{rnd_movie}.mkv";
                                                    System.Diagnostics.Process.Start(path);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Movies\{rnd_movie}.mkv";
                                                        System.Diagnostics.Process.Start(path);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)}\{rnd_movie}.mkv";
                                                            System.Diagnostics.Process.Start(path);
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(0, rnd_movie.Length - 7)}\{rnd_movie}.mkv";
                                                                System.Diagnostics.Process.Start(path);
                                                            }
                                                            catch
                                                            {
                                                                MessageBox.Show($"Cannot find exact file string: {rnd_movie} \nSearch in All\\ Folder", "File Not Found", MessageBoxButtons.OK);
                                                                string path = $@"E:\Media\Movies\All\";
                                                                System.Diagnostics.Process.Start(path);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                        else if (dialogResult == DialogResult.No)
                        {
                            last_dict.Remove($"{rnd_movie}");
                            goto randomizer_A;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Genre selections are too restrictive. No movie fits into these categories: {string.Join(", ", selected_cats)}", "Genre Selections", MessageBoxButtons.OK);
                    }
                }
                else if (and_or == "or")
                {
                    Dictionary<string, string> selected_movies = new Dictionary<string, string>();

                    foreach (string genre in selected_cats)
                    {
                        string filepath = System.IO.Path.Combine(@"E:\Media\Movies\", genre);
                        //MessageBox.Show(filepath);

                        foreach (string movie_folder_path in System.IO.Directory.GetDirectories(filepath))
                        {
                            foreach (string movie_path in System.IO.Directory.GetFiles(System.IO.Path.Combine(filepath, movie_folder_path)))
                            {
                                string movie_name = System.IO.Path.GetFileNameWithoutExtension(movie_path);
                                movie_name = movie_name.Replace(" - Shortcut", "");
                                movie_name = movie_name.Replace(".mkv", "");
                                //movie_name = movie_name.Replace("1. ", "");
                                //movie_name = movie_name.Replace("2. ", "");
                                //movie_name = movie_name.Replace("3. ", "");
                                //movie_name = movie_name.Replace("4. ", "");
                                //movie_name = movie_name.Replace("5. ", "");
                                if (!selected_movies.Keys.Contains(movie_name))
                                {
                                    //MessageBox.Show($"New movie: {movie_name}");
                                    selected_movies.Add(movie_name, movie_path);
                                }
                            }
                        }

                        foreach (string movie_path in System.IO.Directory.GetFiles(filepath))
                        {
                            string movie_name = System.IO.Path.GetFileNameWithoutExtension(movie_path);
                            movie_name = movie_name.Replace(" - Shortcut", "");
                            movie_name = movie_name.Replace(".mkv", "");

                            if (!selected_movies.Keys.Contains(movie_name))
                            {
                                //MessageBox.Show($"New movie: {movie_name}");
                                selected_movies.Add(movie_name, movie_path);
                            }

                        }
                    }
                    if (selected_movies.Keys.Count > 0)
                    {
                    randomizer_B:
                        int movie_dict_length = selected_movies.Keys.Count;

                        DialogResult dialogResult = DialogResult.None;
                        string rnd_movie = "";

                        if (movie_dict_length > 0)
                        {
                            Random num = new Random();
                            int rnd_index = num.Next(movie_dict_length);
                            // MessageBox.Show($"{rnd_index}");
                            // MessageBox.Show($"{String.Join(", ", selected_movies.Keys.ToArray())}");
                            rnd_movie = selected_movies.ElementAt(rnd_index).Key;

                            dialogResult = MessageBox.Show($"How about '{rnd_movie}'?", "Selected Movie", MessageBoxButtons.YesNoCancel);
                        }
                        else
                        {
                            MessageBox.Show("There are no movies left in this category. \nPlease rerun the selection or choose another.", "No More Movies", MessageBoxButtons.OK);
                        }

                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                string path = selected_movies[rnd_movie];
                                System.Diagnostics.Process.Start(path);
                            }
                            catch
                            {
                                try
                                {
                                    string path = $@"E:\Media\Movies\All\{rnd_movie}.mp4";
                                    System.Diagnostics.Process.Start(path);
                                }
                                catch
                                {
                                    try
                                    {
                                        string path = $@"E:\Media\Movies\All\{rnd_movie}\{rnd_movie}.mkv";
                                        System.Diagnostics.Process.Start(path);
                                    }
                                    catch
                                    {
                                        try
                                        {
                                            string path = $@"E:\Media\Movies\All\{rnd_movie}\{rnd_movie}.mp4";
                                            System.Diagnostics.Process.Start(path);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Duology\{rnd_movie}.mkv";
                                                System.Diagnostics.Process.Start(path);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Trilogy\{rnd_movie}.mkv";
                                                    System.Diagnostics.Process.Start(path);
                                                }
                                                catch
                                                {
                                                    try
                                                    {
                                                        string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Quadrology\{rnd_movie}.mkv";
                                                        System.Diagnostics.Process.Start(path);
                                                    }
                                                    catch
                                                    {
                                                        try
                                                        {
                                                            string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)} Movies\{rnd_movie}.mkv";
                                                            System.Diagnostics.Process.Start(path);
                                                        }
                                                        catch
                                                        {
                                                            try
                                                            {
                                                                string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(4, rnd_movie.Length - 7)}\{rnd_movie}.mkv";
                                                                System.Diagnostics.Process.Start(path);
                                                            }
                                                            catch
                                                            {
                                                                try
                                                                {
                                                                    string path = $@"E:\Media\Movies\All\{rnd_movie.Substring(0, rnd_movie.Length - 7)}\{rnd_movie}.mkv";
                                                                    System.Diagnostics.Process.Start(path);
                                                                }
                                                                catch
                                                                {
                                                                    MessageBox.Show($"Cannot find exact file string: {rnd_movie} \nSearch in All\\ Folder", "File Not Found", MessageBoxButtons.OK);
                                                                    string path = $@"E:\Media\Movies\All\";
                                                                    System.Diagnostics.Process.Start(path);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            selected_movies.Remove($"{rnd_movie}");
                            goto randomizer_B;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"No movie exists for these categories: {string.Join(", ", selected_cats)}", "No Movie Selected", MessageBoxButtons.OK);
                    }

                    //MessageBox.Show(string.Join(", ", selected_movies));
                }
            }
            else
            {
                MessageBox.Show("No genres selected. Please make a selection.", "No Genres", MessageBoxButtons.OK);
            }


        }

        private void drama_check_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void all_button_Click(object sender, EventArgs e)
        {
            action_check.Checked = true;
            adventure_check.Checked = true;
            animated_check.Checked = true;
            christmas_check.Checked = true;
            classic_check.Checked = true;
            comedy_check.Checked = true;
            docu_check.Checked = true;
            drama_check.Checked = true;
            epic_check.Checked = true;
            family_check.Checked = true;
            fantasy_check.Checked = true;
            halloween_check.Checked = true;
            historical_check.Checked = true;
            horror_check.Checked = true;
            monster_check.Checked = true;
            musical_check.Checked = true;
            mystery_check.Checked = true;
            romance_check.Checked = true;
            scifi_check.Checked = true;
            superhero_check.Checked = true;
            thriller_check.Checked = true;

            all_button.Visible = false;
            all_button.Enabled = false;
            none_button.Visible = true;
            none_button.Enabled = true;
        }

        private void none_button_Click(object sender, EventArgs e)
        {
            action_check.Checked = false;
            adventure_check.Checked = false;
            animated_check.Checked = false;
            christmas_check.Checked = false;
            classic_check.Checked = false;
            comedy_check.Checked = false;
            docu_check.Checked = false;
            drama_check.Checked = false;
            epic_check.Checked = false;
            family_check.Checked = false;
            fantasy_check.Checked = false;
            halloween_check.Checked = false;
            historical_check.Checked = false;
            horror_check.Checked = false;
            monster_check.Checked = false;
            musical_check.Checked = false;
            mystery_check.Checked = false;
            romance_check.Checked = false;
            scifi_check.Checked = false;
            superhero_check.Checked = false;
            thriller_check.Checked = false;

            all_button.Visible = true;
            all_button.Enabled = true;
            none_button.Visible = false;
            none_button.Enabled = false;
        }

        private void or_radio_tooltip_Popup(object sender, PopupEventArgs e)
        {

        }

        private void sorting_label_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = @"E:\Media\Programs\Movie_Sorting\Movie Sorting\bin\Debug\Movie Sorting.exe";
            System.Diagnostics.Process.Start(path);
        }

        private void musical_check_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void filetransfer_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string path = @"E:\Media\Programs\File_Transfer\File Transfer\bin\Debug\File Transfer.exe";
            System.Diagnostics.Process.Start(path);
        }
    }
}

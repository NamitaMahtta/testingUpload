using Xamarin.Forms;
using MyCHILDApp.DataService;
using System.Collections.Generic;
using MyCHILDApp.Model;
using System.Linq;
using System;
using XLabs.Forms.Controls;

namespace MyCHILDApp
{
    public partial class MyCHILDAppPage : ContentPage
    {
        DataOperation dataOperation;
        IList<VitalForm> item;

        public MyCHILDAppPage()
        {
            InitializeComponent();

            //CheckBox via XLabs.Forms.Controls - not working for iOS - no error but nothing gets displayed
            /*CheckBox CB_NewsLetter = new CheckBox
            {
                DefaultText = "Default text",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                CheckedText = "Ich möchte Newsletter per Mail",
                UncheckedText = "Ich möchte keine Newsletter per Mail",
            };
            StackLayoutTop.Children.Add(CB_NewsLetter);*/

            //Example To add a label from inside C#
            /*var label = new Label
            {
                Text = "Testing Label",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            StackLayoutTop.Children.Add(label);*/

            dataOperation = new DataOperation();
            //Invoke this RefreshData function from here to load the data from service on app launch.
            //RefreshData();
        }

        async void RefreshData()
        {
            //call to the DataService to load data
            item = await dataOperation.GetVitalFormDataAsync();
            //To pre-populate the values in the text boxes
            Temp.Placeholder = item[0].temp;
            HeartRate.Placeholder = item[0].heartRate;
            RespiratoryRate.Placeholder = item[0].respiratoryRate;
            OxygenStaturation.Placeholder = item[0].oxygenSat.ToString();
            PainScore.Placeholder = item[0].painScore.ToString();
            //FeedingStatus.Placeholder = item[0].feedingStatus.ToString();
            //MentalStatus.Placeholder = item[0].mentalStatus;  - changed from text to picker
            Seizure.Placeholder = item[0].seizure.ToString();
            CaregiverWorry.Placeholder = item[0].caregiverWorry.ToString();
            //await DisplayAlert("Alert", "You have been alerted", "OK");
        }

        void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            //lbldisp.Text = String.Format("Slider value is {0:F1}", e.NewValue); 
            //e.ThumbTintColor = UIColor.Blue;
            PainScore.Text = String.Format("{0:F1}", e.NewValue);
        }

        void OnOxySliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            OxygenStaturation.Text = String.Format("{0:F1}", e.NewValue);
        }

        void OnSeizureSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            Seizure.Text = String.Format("{0:F1}", e.NewValue);
        }

        void OnMenStaPickerValueChanged(object sender, ValueChangedEventArgs e){
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                MentalStatus.SelectedItem = (picker.Items[selectedIndex]);
            }  
        }

        SelectMultipleBasePage<CheckItem> multiPage;
        async void OnMultiselectClick(object sender, EventArgs ea)
        {
            var items = new List<CheckItem>();
            items.Add(new CheckItem { Name = "Breathing" });
            items.Add(new CheckItem { Name = "Feeding Status" });
            items.Add(new CheckItem { Name = "Mental Status" });
            items.Add(new CheckItem { Name = "Overall" });

            if (multiPage == null)
                multiPage = new SelectMultipleBasePage<CheckItem>(items) { Title = "Check all that apply" };

            await Navigation.PushAsync(multiPage);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (multiPage != null)
            {
                //results.Text = "";
                CaregiverWorry.Text = "";
                var answers = multiPage.GetSelection();
                foreach (var a in answers)
                {
                    //results.Text += a.Name + ", ";
                    CaregiverWorry.Text += a.Name + ", ";
                    CaregiverWorry.IsVisible = true;
                }
            }
            else
            {
                //results.Text = "(none)";
                CaregiverWorry.Text = "";
            }
        }

        async void Submit_OnClicked(object sender, System.EventArgs e)
        {
            var FeedingStatusInt = 0;

            if(FeedingStatus.SelectedItem.ToString().Trim().Equals("25% of normal feeding")){
                FeedingStatusInt = 1;  
            }else if (FeedingStatus.SelectedItem.ToString().Trim().Equals("50% of normal feeding"))
            {
                FeedingStatusInt = 2;
            }else if (FeedingStatus.SelectedItem.ToString().Trim().Equals("75% of normal feeding"))
            {
                FeedingStatusInt = 3;
            }else {
                FeedingStatusInt = 4;
            }

            VitalForm newItem = new VitalForm
            {
                //DueDate = dpDueDate.Date.ToString("d"),
                //This will throw an error if not valid value is entered
                temp = Temp.Text.Trim(),
                heartRate = HeartRate.Text.Trim(),
                respiratoryRate = RespiratoryRate.Text.Trim(),
                oxygenSat = Convert.ToInt32(OxygenStaturation.Text.Trim()),
                painScore = Convert.ToInt32(PainScore.Text.Trim()),
                //feedingStatus = Convert.ToInt32(FeedingStatus.Text.Trim()),
                feedingStatus = FeedingStatusInt,
                mentalStatus = MentalStatus.SelectedItem.ToString().Trim(),
                //mentalStatus = "no",
                seizure = Convert.ToInt32(Seizure.Text.Trim()),
                //seizure = "0",
                caregiverWorry = CaregiverWorry.Text.Trim()
            };
            await dataOperation.AddTodoItemAsync(newItem);
            await DisplayAlert("Alert", "Data added!", "OK");
            //RefreshData();
        }

        public async void OnDone(object sender, EventArgs e)
        {
            /*var mi = ((MenuItem)sender);
            VitalForm itemToUpdate = (VitalForm)mi.CommandParameter;
            //itemToUpdate.isDone = true;
            itemToUpdate.isDone = 1;
            int itemIndex = items.IndexOf(itemToUpdate);
            await dataOperation.UpdateTodoItemAsync(itemIndex, itemToUpdate);*/
            RefreshData();
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            /*var mi = ((MenuItem)sender);
            VitalForm itemToDelete = (VitalForm)mi.CommandParameter;
            int itemIndex = items.IndexOf(itemToDelete);
            await dataOperation.DeleteTodoItemAsync(itemIndex);*/
            RefreshData();
        }
    }
}

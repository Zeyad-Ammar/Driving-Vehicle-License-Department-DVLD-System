using DVLD_Project.Applications.DetainAndReleaseLicense;
using DVLD_Project.Applications.ReplaceLicenseForDamagOrLost;
using DVLD_Project.General;
using DVLD_Project.Licenses.InternationalLicenses;
using DVLD_Project.Licenses.LocalLicenses;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class MainForm : Form
    {
        private frmLogin _LoginForm=null;
        public MainForm(frmLogin LoginForm)
        {
            InitializeComponent();
            _LoginForm = LoginForm;
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPeople PeopleForm = new frmPeople();
            PeopleForm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsers UsersForm = new frmUsers();
            UsersForm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _LoginForm.Show();
            this.Close();

            
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword changePasswordForm = new frmChangePassword(clsCurrentUser.User.UserID);
            changePasswordForm.ShowDialog();

        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserCard userCardForm=new frmUserCard(clsCurrentUser.User.UserID);
            userCardForm.ShowDialog();


        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageApplicationTypes manageApplicationTypesForm = new frmManageApplicationTypes();
            manageApplicationTypesForm.ShowDialog();

        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageTestTypes manageTestTypesForm = new frmManageTestTypes(); 
            manageTestTypesForm.ShowDialog();

        }

        private void localDrivingLicensAppsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications localDrivingLicenseApplicationsForm= new frmLocalDrivingLicenseApplications();
            localDrivingLicenseApplicationsForm.ShowDialog();
        }

        private void accountSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditLocalLicense localLicenseForm = new frmAddEditLocalLicense();
            localLicenseForm.ShowDialog();
           
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDrivers driversForm = new frmDrivers();
            driversForm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseApplications localDrivingLicenseApplicationsForm = new frmLocalDrivingLicenseApplications();
            localDrivingLicenseApplicationsForm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicense internationalLicenseForm = new frmInternationalLicense();
            internationalLicenseForm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageInternationalApps manageInternationalAppsForm = new frmManageInternationalApps();
            manageInternationalAppsForm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRenewLicense renewLicenseForm = new frmRenewLicense();
            renewLicenseForm.ShowDialog();
        }

        private void replacementForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReplaceLicense replaceLicenseForm = new frmReplaceLicense();
            replaceLicenseForm.ShowDialog();
        }

        private void manageDetainedLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmManageDetainedLicenses manageDetainedLicensesForm = new frmManageDetainedLicenses();
            manageDetainedLicensesForm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDetainLicense detainLicenseForm = new frmDetainLicense();
            detainLicenseForm.ShowDialog();
        }

        private void releaseLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense releaseDetainedLicenseForm = new frmReleaseDetainedLicense();
            releaseDetainedLicenseForm.ShowDialog();
        }

        private void releaseDetainedDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReleaseDetainedLicense releaseDetainedLicenseForm = new frmReleaseDetainedLicense();
            releaseDetainedLicenseForm.ShowDialog();
        }
    }
}

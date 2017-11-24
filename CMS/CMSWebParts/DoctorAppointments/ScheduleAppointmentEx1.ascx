<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ScheduleAppointmentEx1.ascx.cs" Inherits="CMSWebParts_DoctorAppointments_ScheduleAppointmentEx1" %>

<div class="ui raised very padded text container segment">
    <h2 class="ui header">Schedule an appointment</h2>

    <asp:PlaceHolder runat="server" ID="plcErrorMessage" Visible="false">
        <div class="ui error message">
            <i class="close icon"></i>
            <div class="header">
                There were some errors with your submission
            </div>
            <asp:BulletedList runat="server" ID="ErrorList"
                CssClass="list"
                DisplayMode="Text">
            </asp:BulletedList>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="plcSuccessMessage" Visible="false">
        <div class="ui success message">
            <div class="header">
                Your appointment submission was successful.
            </div>
            <p>You will soon receive an e-mail confirmation.</p>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="plcAppointmentForm">
        <div class="ui form">
            <div class="field">
                <label>First Name*</label>
                <input runat="server" type="text" id="FirstName" placeholder="First Name">
            </div>
            <div class="field">
                <label>Last Name*</label>
                <input runat="server" type="text" id="LastName" placeholder="Last Name">
            </div>
            <div class="field">
                <label>E-mail*</label>
                <input runat="server" type="email" id="Email" placeholder="E-mail">
            </div>
            <div class="field">
                <label>Date of Birth*</label>
                <input runat="server" type="text" id="DateOfBirth" class="DateOfBirth">
            </div>
            <div class="field">
                <label>Date of appointment*</label>
                <input runat="server" type="text" id="DateOfAppointment" class="DateOfAppointment">
            </div>
            <div class="field">
                <label>Doctor*</label>
                <asp:DropDownList runat="server" ID="SelectDoctor" CssClass="ui dropdown SelectDoctor" EnableViewState="true">
                    <asp:ListItem Text="Select doctor" Value="" />
                </asp:DropDownList>
            </div>
            <div class="field">
                <label>Telephone number (U.S. only)</label>
                <input runat="server" type="text" id="TelephoneNumber">
            </div>
            <asp:Button runat="server" ID="Submit" class="ui button" Text="Submit" OnClick="Submit_Click"></asp:Button>
        </div>
    </asp:PlaceHolder>
</div>

<script type="text/javascript">
    $(function () {
        // initialize dropdown menu for Doctors field
        $('.SelectDoctor')
            .dropdown();

        // close error window
        $('.message .close')
            .on('click', function () {
                $(this).closest('.message').transition('fade');
            });

        // initialize date picker for DateOfBirth and DateOfAppointment fields
        $(".DateOfAppointment, .DateOfBirth").datepicker({
            changeMonth: true,
            changeYear: true
        });
    });
</script>



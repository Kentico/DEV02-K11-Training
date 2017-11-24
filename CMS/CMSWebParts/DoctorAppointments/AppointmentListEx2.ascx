<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AppointmentListEx2.ascx.cs" Inherits="CMSWebParts_DoctorAppointments_AppointmentListEx2" %>

<div class="ui raised very padded text container segment">
    <h2 class="ui header">Appointment list</h2>
    <div class="ui relaxed divided list">
        <asp:Repeater runat="server" ID="repAppointments">
            <ItemTemplate>
                <div class="item">
                    <i class="large doctor middle aligned icon"></i>
                    <div class="content">
                        <a class="header"><%# Eval("DoctorFullName") %></a>
                        <div class="description">Has an appointment with <strong><%# Eval("AppointmentPatientFullName") %></strong> at <%# Eval("AppointmentDate") %></div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>

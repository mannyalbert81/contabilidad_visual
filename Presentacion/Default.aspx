<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentacion.Default" %>
 <%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register assembly="GMaps" namespace="Subgurim.Controles" tagprefix= "cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/Site.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div>
            <cc1:GMap ID="GMap" runat="server" enableGoogleBar="True"
            enableHookMouseWheelToZoom="True"
            enableServerEvents="True" Height="600px"
            Version="3" Width="940px" enableGKeyboardHandler="True"
            serverEventsType="AspNetPostBack"
            Key="AIzaSyBSUyfsnhzUlOfseRHxAfEP_4T9UTgZStU"
            enableStore="False" enableGetGMapElementById="True"
            enableDragging="True" enableGTrafficOverlay="True" mapType="Satellite"  GZoom="1" />
           </div>
</asp:Content>

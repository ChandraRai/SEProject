<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Flashminder.About" %>
<%@ Register Src="~/Custom_User_Controls/Jumbotron.ascx" TagName="Jumbotron" TagPrefix="cusjum" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-9">
                <cusjum:Jumbotron runat="server"></cusjum:Jumbotron>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <div class="panel-title">
                            Team Members
                        </div>
                    </div>
                    <div class="panel-body">
                        <address>
                            <strong>Chandra Rai</strong><br />
                            400351588<br />
                            <abbr title="Email">raic1@mcmaster.ca</abbr><br />
                        </address>
                        <address>
                            <strong>Emily Goodwin</strong><br />
                            400352097<br />
                            <abbr title="Email">goodwe2@mcmaster.ca</abbr><br />
                        </address>
                        <address>
                            <strong>Stefan Slehta</strong><br />
                            400291135<br />
                            <abbr title="Email">slehtas@mcmaster.ca</abbr><br />
                        </address>
                        <address>
                            <strong>Violet Huang</strong><br />
                            400348357<br />
                            <abbr title="Email">huanx34@mcmaster.ca</abbr><br />
                        </address>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

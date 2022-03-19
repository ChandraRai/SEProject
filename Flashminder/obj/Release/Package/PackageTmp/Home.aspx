<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Flashminder.Home" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Repeater ID="rptrUserProfile" runat="server">
        <ItemTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-md-offset-4 col-md-4">
                        <h3>Hello! <%#Eval("Username")%>
                        </h3>
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <div class="panel-title">
                                    Profile
                                </div>
                            </div>
                            <div class="panel-body">
                                <img src="Assets/avatar.png" alt="Avatar" class="avatar" style="width: 100px; height: 100px;">
                                <address>
                                    <strong><%#Eval("Username")%></strong><br />
                                    <abbr title="Email"><%#Eval("Email")%></abbr><br />
                                </address>
                                <span id="test"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <div class="container">
        <div class="row">
            <div class="col-md-offset-4 col-md-4">
                <div class="text-right">
                    <asp:Button runat="server" ID="Quiz" CssClass="btn btn-info" Text="Start Quiz" OnClick="StartQuiz_Click"/>
                </div>
            </div>
        </div>
    </div>
    <br />
</asp:Content>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="Flashminder.Custom_User_Controls.Navbar" %>

<!-- This section is navigation bar for each content page -->
<nav class="navbar navbar-default" role="navigation">
    <div class="container-fluid">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="About.aspx"><i class="fa fa-book fa-lg"></i> Flashminder</a>
        </div>

        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-right">
                <asp:PlaceHolder ID="PrivatePlaceHolder" runat="server">
                    <li id="home" runat="server"><a href="Home.aspx"><i class="fa fa-home fa-lg"></i> Home</a></li>
                    <li id="dashboard" runat="server"><a href="ViewDashboard.aspx"><i class="fa fa-bar-chart fa-lg"></i> Dashboard</a></li>
                    <li id="logout" runat="server"><a href="Logout.aspx"><i class="fa fa-sign-out fa-lg"></i> Logout</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="PublicPlaceHolder" runat="server">
                    <li id="signin" runat="server"><a href="Signin.aspx"><i class="fa fa-user fa-lg"></i> Sign in</a></li>
                    <li id="signup" runat="server"><a href="Signup.aspx"><i class="fa fa-edit fa-lg"></i> Sign up</a></li>
                    <li id="about" runat="server"><a href="Default.aspx"><i class="fa fa-info fa-lg"></i> About</a></li>
                </asp:PlaceHolder>
            </ul>
        </div>
        <!-- /.navbar-collapse -->
    </div>
    <!-- /.container-fluid -->
</nav>

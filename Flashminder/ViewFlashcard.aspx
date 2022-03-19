<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ViewFlashcard.aspx.cs" Inherits="Flashminder.ViewFlashcard" %>

<!DOCTYPE html>
<head>
    <title>Flashcard</title>
    <link rel="stylesheet" href="Content/bootstrap.css" />
</head>

<script type="text/javascript" src="Scripts/jquery-2.2.3.js"></script>
<style>
    /*https://www.w3schools.com/howto/howto_css_flip_card.asp */
    /* The flip card container - set the width and height to whatever you want. We have added the border property to demonstrate that the flip itself goes out of the box on hover (remove perspective if you don't want the 3D effect */
    .flip-card {
        background-color: transparent;
        width: 700px;
        height: 400px;
        border: 1px solid #f1f1f1;
        perspective: 1000px; /* Remove this if you don't want the 3D effect */
    }

    /* This container is needed to position the front and back side */
    .flip-card-inner {
        position: absolute;
        width: 100%;
        height: 100%;
        text-align: center;
    }

    /* Position the front and back side */
    .flip-card-front, .flip-card-back {
        position: absolute;
        width: 100%;
        height: 100%;
        -webkit-backface-visibility: hidden; /* Safari */
        backface-visibility: hidden;
        transition: transform 0.8s;
        transform-style: preserve-3d;
    }

    .flip {
        transform: rotateY(180deg);
    }

    /* Style the front side (fallback if image is missing) */
    .flip-card-front {
        margin-top: 25px;
        background-color: black;
        color: white;
        border-radius: 20px;
    }

    /* Style the back side */
    .flip-card-back {
        margin-top: 25px;
        background-color: black;
        color: white;
        border-radius: 20px;
    }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        $(".flip-card").click(function () {

            if ($('.front-card').hasClass("flip")) {
                $('.back-card').addClass("flip");
                $('.front-card').removeClass("flip");
            }
            else {
                $('.front-card').addClass("flip");
                $('.back-card').removeClass("flip");
            }
        });
    });
</script>

<body>
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <div class="flip-card">
                    <div class="flip-card-inner">
                        <div class="front-card flip-card-front">
                            <div class="front-image ">
                                <asp:Image ID="front_image" Style="margin-top: 25px;" runat="server" Height="300" Width="500" />
                            </div>
                            <div class="front-text">
                                <asp:Literal ID="front_text" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="back-card flip-card-back flip">
                            <div class="back-image ">
                                <asp:Image Style="margin-top: 25px;" ID="back_image" runat="server" Height="300" Width="500" />
                            </div>
                            <div class="back-text">
                                <asp:Literal ID="back_text" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="text-left" style="margin-top: 40px;">
                    <a href="ViewFlashcards.aspx" class="btn btn-default">Back</a>
                </div>

            </div>
        </div>
    </div>
</body>

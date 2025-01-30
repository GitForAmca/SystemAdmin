(function () {
    window.requestAnimFrame = (function (callback) {
        return window.requestAnimationFrame ||
            window.webkitRequestAnimationFrame ||
            window.mozRequestAnimationFrame ||
            window.oRequestAnimationFrame ||
            window.msRequestAnimaitonFrame ||
            function (callback) {
                window.setTimeout(callback, 1000 / 60);
            };
    })();

    var canvas = document.getElementById("sig-canvas");
    var canvas1 = document.getElementById("blankcanvas");
    var ctx = canvas.getContext("2d");
    ctx.strokeStyle = "#222222";
    ctx.lineWidth = 4;

    var drawing = false;
    var mousePos = {
        x: 0,
        y: 0
    }; 
    var lastPos = mousePos;

    canvas.addEventListener("mousedown", function (e) {
        drawing = true;
        lastPos = getMousePos(canvas, e);
    }, false);

    canvas.addEventListener("mouseup", function (e) {
        drawing = false;
    }, false);

    canvas.addEventListener("mousemove", function (e) {
        mousePos = getMousePos(canvas, e);
    }, false);

    // Add touch event support for mobile
    canvas.addEventListener("touchstart", function (e) {

    }, false);

    canvas.addEventListener("touchmove", function (e) {
        var touch = e.touches[0];
        var me = new MouseEvent("mousemove", {
            clientX: touch.clientX,
            clientY: touch.clientY
        });
        canvas.dispatchEvent(me);
    }, false);

    canvas.addEventListener("touchstart", function (e) {
        mousePos = getTouchPos(canvas, e);
        var touch = e.touches[0];
        var me = new MouseEvent("mousedown", {
            clientX: touch.clientX,
            clientY: touch.clientY
        });
        canvas.dispatchEvent(me);
    }, false);

    canvas.addEventListener("touchend", function (e) {
        var me = new MouseEvent("mouseup", {});
        canvas.dispatchEvent(me);
    }, false);

    function getMousePos(canvasDom, mouseEvent) {
        var rect = canvasDom.getBoundingClientRect();
        return {
            x: mouseEvent.clientX - rect.left,
            y: mouseEvent.clientY - rect.top
        }
    }

    function getTouchPos(canvasDom, touchEvent) {
        var rect = canvasDom.getBoundingClientRect();
        return {
            x: touchEvent.touches[0].clientX - rect.left,
            y: touchEvent.touches[0].clientY - rect.top
        }
    }

    function renderCanvas() {
        if (drawing) {
            ctx.moveTo(lastPos.x, lastPos.y);
            ctx.lineTo(mousePos.x, mousePos.y);
            ctx.stroke();
            lastPos = mousePos;
        }
    }

    // Prevent scrolling when touching the canvas
    document.body.addEventListener("touchstart", function (e) {
        if (e.target == canvas) {
            e.preventDefault();
        }
    }, false);
    document.body.addEventListener("touchend", function (e) {
        if (e.target == canvas) {
            e.preventDefault();
        }
    }, false);
    document.body.addEventListener("touchmove", function (e) {
        if (e.target == canvas) {
            e.preventDefault();
        }
    }, false);

    (function drawLoop() {
        requestAnimFrame(drawLoop);
        renderCanvas();
    })();

    function clearCanvas() {
        canvas.width = canvas.width;
    }

    // Set up the UI for EL to Client
    var sigTextClient = document.getElementById("hdfSignatureURLClient");
    var clearBtnClient = document.getElementById("sig-clearBtn");
    var submitBtnClient = document.getElementById("btnUpload");
    clearBtnClient.addEventListener("click", function (e) {
        clearCanvas();
        ctx.lineWidth = 4;
    }, false);
    submitBtnClient.addEventListener("click", function (e) {
        debugger
        var can1 = canvas.toDataURL();
        var can2 = canvas1.toDataURL();
        var checkedRadio = $("input[type='radio'][name='UploadDrawSignature']:checked").val();
        if (checkedRadio == "Upload Signature") {
            sigTextClient.value = "";
            return false;
        }
        else if (checkedRadio == "Draw Signature") {
            if (can1 == can2) {
                document.getElementById("sig-canvas").style.border = "2px dotted red";
                e.preventDefault();
            }
            else {
                sigTextClient.value = can1;
                return true;
            }
        }
        else {
            if (can1 == can2) {
                document.getElementById("sig-canvas").style.border = "2px dotted red";
                e.preventDefault();
            }
            else {
                sigTextClient.value = can1;
                return true;
            }
        }
        
    }, true);

    $('input[type=radio][name=UploadDrawSignature]').change(function () {
        if (this.value == "Upload Signature") {
            clearCanvas();
            ctx.lineWidth = 4;
        }
    });

})();
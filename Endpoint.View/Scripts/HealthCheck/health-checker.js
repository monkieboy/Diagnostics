var checker = {};
checker.init = function() {
    $(function() {
        checker.loadFields('#fields');
        checker.configureVerifications('localhost', checker.solutionType.webServer);
        checker.configureVerifications('blab', checker.solutionType.connection);
        checker.configureVerifications('random', checker.solutionType.webServer);
    });
};

checker.solutionType = {
    webServer: "webserver",
    connection: "connection"
};

function htmlEncode(value) {
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

checker.loadFields = function (field) {
    $.get('http://localhost:41100/', function (data) {
        var value = eval('(' + data + ')');
        var concat = '';

        value.forEach(function (entry) {
            if (entry != '') {
                var temp = '<div id="check-' + entry +'">' +
                    'State not verified' +
                '</div>\r';
                concat += temp;
            }
        });

        $(field).html(concat);
    });
};

checker.configureVerifications = function (t, solutionType) {
    var valueToVerify = t;
     

    var verify = function () {

        $('#check-' + valueToVerify).load('http://localhost:41100/' + valueToVerify, null, function (text, status) {
            var response = eval('(' + text + ')');
            $('#check-' + valueToVerify).css('border', 'solid 3px ' + response.Color);
            $('#check-' + valueToVerify).css('border-radius', '15px');

            $('#check-' + valueToVerify).css('display', 'inline-block');

            if (solutionType == "webserver") {
                $('#check-' + valueToVerify).css('height', '125px');
                $('#check-' + valueToVerify).css('width', '124px');
                $('#check-' + valueToVerify).css('padding-left', '10px');
                $('#check-' + valueToVerify).css('margin', '10px 10px 10px 10px');
                $('#check-' + valueToVerify).css('background', 'url(/content/Images/webserver.jpg) no-repeat 30px 25px');
                $('#check-' + valueToVerify).text('Running: ' + response.State);
            } else {
                $('#check-' + valueToVerify).css('height', '44px');
                $('#check-' + valueToVerify).css('width', '510px');
                $('#check-' + valueToVerify).css('background', 'url(/content/Images/rightarrow.jpg) no-repeat 2px 15px');
                $('#check-' + valueToVerify).text('Available: ' + response.State);
            }
        }).fadeIn("slow");
    };
    setInterval(verify, 2000);
};

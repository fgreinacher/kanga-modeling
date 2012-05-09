/// <reference path="jquery-1.5.1.js" />

kanga = function (apiBaseUri) {

  var _apiBaseUri = apiBaseUri;

  var _client = function () {

    this.arguments = function (source, type, style) {

      return { source: encodeURI(source), type: type, style: style };

    }

    this.createDiagram = function (arguments, callback) {

      $.getJSON(
        _apiBaseUri + '/create', arguments, function (result) {

          callback(result);

        });
    }

  }

  var _highlighter = function (client) {

    var _client = client;

    this.replaceAll = function () {

      $('pre.kanga').each(function (i, element) {

        replace(element);

      });

    }

    var replace = function (element) {

      var source = $(element).text();
      var type = $(element).data('kanga-type');
      var style = $(element).data('kanga-style');

      var arguments = _client.arguments(source, type, style);

      _client.createDiagram(arguments, function (result) {

        var information = '';
        information += 'Source:\r\n';
        information += result.source;

        if (result.errors.length > 0) {

          information += '\r\n';
          information += 'Errors:\r\n';

          $(result.errors).each(function (index, error) {

            information += error.message;
            information += '(';
            information += 'line ' + error.token.line;
            information += ', start ' + error.token.start;
            information += ', end ' + error.token.end;
            information += ', value ' + error.token.value;
            information += ')';
            information += '\r\n';

          });

        } else {

          information += '\r\n';
          information += 'No errors';

        }


        var img = $('<img></img>')
          .attr('src', result.diagram)
          .attr('title', information)
          .attr('alt', information)
          .css('display', 'block');

        $(element).after(img);
        $(element).hide();

      });

    }

  }

  this.client = new _client();
  this.highlighter = new _highlighter(this.client);

}

var _kanga = new kanga('_KANGA_API_BASE_URI_');
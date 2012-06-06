/// <reference path="jquery-1.5.1.js" />

jQuery.noConflict();

Kanga = function (apiBaseUri, $) {

  var localClient = function () {

    this.createDiagram = function (args, callback) {

      args = { source: encodeURI(args.source), type: args.type, style: args.style };

      $.get(apiBaseUri + 'create', args, function (result) {

        callback(result);

      }, 'json');

    };
  };

  var localHighlighter = function (client) {

    var thisHighlighter = this;

    this.replaceAll = function () {

      $('pre.kanga').each(function (i, element) {

        thisHighlighter.replace(element);

      });

    };

    this.replace = function (element) {

      var source = $(element).text();
      var type = $(element).data('kanga-type');
      var style = $(element).data('kanga-style');

      var arguments = { source: source, type: type, style: style };

      client.createDiagram(arguments, function (result) {

        var information = buildInformation(result);
        var img = $('<img></img>')
          .attr('src', result.diagram)
          .attr('width', result.diagramWidth)
          .attr('height', result.diagramHeight)
          .attr('title', information)
          .attr('alt', information)
          .css('display', 'block');

        $(element).after(img);
        $(element).hide();

      });

    };
    var buildInformation = function (result) {

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

      return information;

    };
  };

  this.client = new localClient();

  this.highlighter = new localHighlighter(this.client);

};

var kanga = new Kanga('_KANGA_API_BASE_URI_', jQuery);

jQuery(document).ready(function () {

  kanga.highlighter.replaceAll();

});
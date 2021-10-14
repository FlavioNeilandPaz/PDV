import 'package:fenix/src/model/filtro.dart';
import 'package:fenix/src/model/retorno_json_erro.dart';

/// configuração base para os serviços REST
class ServiceBase {
  static const _porta = '80';
  static const _enderecoServidor = 'http://7ad3b690.ngrok.io';
  static const _endpoint = _enderecoServidor + ':' + _porta;
  get endpoint => _endpoint;

  static var _url = '';
  get url => _url;

  static var _objetoJsonErro = RetornoJsonErro();
  get objetoJsonErro => _objetoJsonErro;

  // o filtro deve ser enviado da seguinte forma: ?filter=field||$condition||value
  // referência: https://github.com/nestjsx/crud/wiki/Requests
  void tratarFiltro(Filtro filtro, String entidade) {
    var stringFiltro = '';

    if (filtro != null) {
      stringFiltro = '?filter=' + filtro.campo + '||\$cont' + '||' + filtro.valor;
    }

    _url = _endpoint + entidade + stringFiltro;
  }

  void tratarRetorno(Map<String, dynamic> body) {
    _objetoJsonErro.status = body['status']?.toString() ?? body['statuscode'].toString();
    _objetoJsonErro.error = body['error'] ?? body['classname'].toString();
    _objetoJsonErro.message = body['message'];
    _objetoJsonErro.trace = body['trace'];
  }

}
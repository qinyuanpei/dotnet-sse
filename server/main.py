import http.server
import socketserver
import socket
class HTTPServerV6(http.server.HTTPServer):
  address_family = socket.AF_INET6

port = 8888
Handler = http.server.SimpleHTTPRequestHandler
server = HTTPServerV6(('::',port), Handler)
print('HTTPServer is running on ' + str(port))
server.serve_forever()


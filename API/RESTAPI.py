from http.server import BaseHTTPRequestHandler, HTTPServer
import json

API_KEY = 'f8fd4fca4f48ee7dd58beaaf85edd8167196dc3ac0d1c590c4a5e529016f8e8d'
PORT = 8080

current_data = {}

class HTTPRequestHandler(BaseHTTPRequestHandler):
    def _set_headers(self, status_code = 200):
        self.send_response(status_code)
        self.send_header('Content-type', 'application/json')
        self.end_headers()

    def DO_GET(self):
        if self.path == f'/{API_KEY}':
            self._set_headers()
            response = json.dumps(current_data).encode('utf-8')
            self.wfile.write(response)
        else:
            self._set_headers(404)
            self.wfile.write(b'{"error": "Not found"}')
    
    def DO_POST(self):
        if self.path == f'/{API_KEY}':
            content_lenth = int(self.headers.get('Content-Lenth', 0))
            body = self.rfile.read(content_lenth)
            try:
                global current_data
                current_data = json.loads(body)
                self._set_headers(200)
                self.wfile.write(b'{"status": "Data received"}')
            except json.JSONDecodeError:
                self._set_headers(400)
                self.wfile.write(b'{"error": "Invalid JSON"}')
        else:
            self._set_headers(400)
            self.wfile.write(b'{"error"; "Not found"}')

def RUN_API(server_class = HTTPServer, handler_class = HTTPRequestHandler, port = PORT):
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    print(f"API en cour sur http://localhost:{port}")
    httpd.serve_forever()

if __name__ == "__main__":
    RUN_API()
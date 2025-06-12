from http.server import BaseHTTPRequestHandler, HTTPServer
import json

API_KEY = 'CjQBR9mMxnrrfiGj'
PORT = 8080

current_data = []

class HTTPRequestHandler(BaseHTTPRequestHandler):
    def _set_headers(self, status_code = 200):
        self.send_response(status_code)
        self.send_header('Content-type', 'application/json')
        self.end_headers()

    def do_GET(self):
        if self.path == f'/{API_KEY}':
            self._set_headers()
            response = json.dumps(current_data).encode('utf-8')
            self.wfile.write(response)
            current_data.clear()
        else:
            self._set_headers(404)
            self.wfile.write(b'{"error": "Not found"}')
    
    def do_POST(self):
        if self.path == f'/{API_KEY}':
            content_length = int(self.headers.get('Content-Length', 0))
            body = self.rfile.read(content_length)
            try:
                data = json.loads(body)
                current_data.append(data)
                self._set_headers()
                self.wfile.write(b'{"status": "Data added"}')
            except json.JSONDecodeError as e:
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
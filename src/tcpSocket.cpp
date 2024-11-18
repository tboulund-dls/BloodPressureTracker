#include "headers/tcpSocket.h"

TCPServer::TCPServer(int port)
{
    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY;
    address.sin_port = htons(port);
}

TCPServer::~TCPServer()
{
  stop();
}


bool TCPServer::start() {
    server_fd = socket(AF_INET, SOCK_STREAM, 0);
    if (server_fd == 0) {
        perror("Socket creation failed");
        return false;
    }

    // Step 2: Bind the socket to the specified port
    int opt = 1;
    if (setsockopt(server_fd, SOL_SOCKET, SO_REUSEADDR | SO_REUSEPORT, &opt, sizeof(opt)) < 0) {
        perror("setsockopt failed");
        stop();
        return false;
    }

    if (bind(server_fd, (struct sockaddr *)&address, sizeof(address)) < 0) {
        perror("Bind failed");
        stop();
        return false;
    }

    if (listen(server_fd, 3) < 0) {
        perror("Listen failed");
        stop();
        return false;
    }

    std::cout << "Server is listening on port " << port << "..." << std::endl;
    is_running = true;

    return true;
}

void TCPServer::stop() {
    if (client_fd != -1) {
        close(client_fd);
        client_fd = -1;
    }
    if (server_fd != -1) {
        close(server_fd);
        server_fd = -1;
    }
    is_running = false;
}

void TCPServer::handleClient() {
    socklen_t addrlen = sizeof(address);

    while (is_running) {
        std::cout << "Waiting for a client to connect..." << std::endl;

        // Step 4: Accept an incoming connection
        client_fd = accept(server_fd, (struct sockaddr *)&address, &addrlen);
        if (client_fd < 0) {
            perror("Accept failed");
            stop();
            return;
        }

        std::cout << "Client connected" << std::endl;

        // Step 5: Communicate with the client
        char buffer[1024] = {0};
        int valread = read(client_fd, buffer, sizeof(buffer));
        std::cout << "Message received: " << buffer << std::endl;

        // Respond to the client
        const char *response = "Hello from server";
        send(client_fd, response, strlen(response), 0);
        std::cout << "Response sent" << std::endl;

        // Close the client connection
        close(client_fd);
        client_fd = -1;
    }
}


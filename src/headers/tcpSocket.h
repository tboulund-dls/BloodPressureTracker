#pragma once

#include <iostream>
#include <cstring>
#include <sys/socket.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <unistd.h>

class TCPServer {
public:
    TCPServer(int port);
    ~TCPServer();

    bool start();
    void stop();
    void handleClient();

private:
    int server_fd, client_fd;
    struct sockaddr_in address;
    int port;
    bool is_running;
};
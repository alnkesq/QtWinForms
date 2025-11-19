#include <QApplication>
#include <QWidget>
#include <iostream>

int main(int argc, char** argv) {
    std::cout << "Creating QApplication..." << std::endl;
    QApplication app(argc, argv);
    std::cout << "QApplication created!" << std::endl;
    
    std::cout << "Creating QWidget..." << std::endl;
    QWidget window;
    window.setWindowTitle("Test");
    window.show();
    std::cout << "QWidget shown!" << std::endl;
    
    return app.exec();
}

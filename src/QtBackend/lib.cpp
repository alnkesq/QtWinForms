#include <QApplication>
#include <QWidget>
#include <QPushButton>

#ifdef _WIN32
#define EXPORT __declspec(dllexport)
#else
#define EXPORT
#endif

extern "C" {
    EXPORT void* QApplication_Create() {
        static int argc = 1;
        static char* argv[] = { (char*)"TestApp", nullptr };
        return new QApplication(argc, argv);
    }
    
    EXPORT void QApplication_Run() {
        QApplication::exec();
    }
    
    EXPORT void* QWidget_Create(void* parent) {
        return new QWidget((QWidget*)parent);
    }
    
    EXPORT void QWidget_Show(void* widget) {
        if (widget) ((QWidget*)widget)->show();
    }
    
    EXPORT void QWidget_SetParent(void* widget, void* parent) {
        if (widget) ((QWidget*)widget)->setParent((QWidget*)parent);
    }
    
    EXPORT void QWidget_SetTitle(void* widget, const char* title) {
        if (widget) ((QWidget*)widget)->setWindowTitle(QString::fromUtf8(title));
    }
    
    EXPORT void* QPushButton_Create(void* parent, const char* text) {
        QPushButton* btn = new QPushButton(QString::fromUtf8(text), (QWidget*)parent);
        return btn;
    }
    
    EXPORT void QPushButton_SetText(void* button, const char* text) {
        if (button) ((QPushButton*)button)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void QPushButton_ConnectClicked(void* button, void (*callback)(void*), void* userData) {
        if (button) {
            QPushButton* btn = (QPushButton*)button;
            QObject::connect(btn, &QPushButton::clicked, [callback, userData]() {
                callback(userData);
            });
        }
    }
}

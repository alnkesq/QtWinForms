#include <QApplication>
#include <QWidget>

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
    
    EXPORT void QWidget_SetTitle(void* widget, const char* title) {
        if (widget) ((QWidget*)widget)->setWindowTitle(QString::fromUtf8(title));
    }
}

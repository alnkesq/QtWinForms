#include <QApplication>
#include <QWidget>
#include <QPushButton>
#include <QLabel>

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
        ((QWidget*)widget)->show();
    }
    
    EXPORT void QWidget_SetParent(void* widget, void* parent) {
        ((QWidget*)widget)->setParent((QWidget*)parent);
    }
    
    EXPORT void QWidget_Move(void* widget, int x, int y) {
        ((QWidget*)widget)->move(x, y);
    }
    
    EXPORT void QWidget_Resize(void* widget, int width, int height) {
        ((QWidget*)widget)->resize(width, height);
    }
    
    EXPORT void QWidget_SetTitle(void* widget, const char* title) {
        ((QWidget*)widget)->setWindowTitle(QString::fromUtf8(title));
    }
    
    EXPORT void* QPushButton_Create(void* parent, const char* text) {
        QPushButton* btn = new QPushButton(QString::fromUtf8(text), (QWidget*)parent);
        return btn;
    }
    
    EXPORT void QPushButton_SetText(void* widget, const char* text) {
        ((QPushButton*)widget)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void QPushButton_ConnectClicked(void* widget, void (*callback)(void*), void* userData) {
        QPushButton* btn = (QPushButton*)widget;
        QObject::connect(btn, &QPushButton::clicked, [callback, userData]() {
            callback(userData);
        });
    }
    
    EXPORT void* QLabel_Create(void* parent, const char* text) {
        QLabel* label = new QLabel(QString::fromUtf8(text), (QWidget*)parent);
        return label;
    }
    
    EXPORT void QLabel_SetText(void* widget, const char* text) {
        ((QLabel*)widget)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void QWidget_SetBackColor(void* widget, unsigned char r, unsigned char g, unsigned char b, unsigned char a) {
        QWidget* w = (QWidget*)widget;
        // If alpha is 0 (Color.Empty), don't set background to preserve Qt's native look
        if (a == 0) {
            return;
        }
        QPalette palette = w->palette();
        palette.setColor(QPalette::Window, QColor(r, g, b, a));
        w->setAutoFillBackground(true);
        w->setPalette(palette);
    }
}

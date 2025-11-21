#include <QApplication>
#include <QWidget>
#include <QPushButton>
#include <QLabel>
#include <QCheckBox>
#include <QLineEdit>
#include <QEvent>
#include <QResizeEvent>
#include <QMoveEvent>
#include <iostream>
using namespace std;
#ifdef _WIN32
#define EXPORT __declspec(dllexport)
#else
#define EXPORT
#endif

extern "C" {

    typedef void (*ReadQStringCallback)(const void* dataUtf16, int length, void* userData);

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
        QPushButton* widget = new QPushButton(QString::fromUtf8(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QPushButton_SetText(void* widget, const char* text) {
        ((QPushButton*)widget)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void QPushButton_ConnectClicked(void* widget, void (*callback)(void*), void* userData) {
        QObject::connect((QPushButton*)widget, &QPushButton::clicked, [callback, userData]() {
            callback(userData);
        });
    }
    
    EXPORT void* QLabel_Create(void* parent, const char* text) {
        QLabel* widget = new QLabel(QString::fromUtf8(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QLabel_SetText(void* label, const char* text) {
        ((QLabel*)label)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void* QCheckBox_Create(void* parent, const char* text) {
        QCheckBox* widget = new QCheckBox(QString::fromUtf8(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QCheckBox_SetText(void* widget, const char* text) {
        ((QCheckBox*)widget)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void QCheckBox_SetChecked(void* widget, bool isChecked) {
        ((QCheckBox*)widget)->setChecked(isChecked);
    }
    
    EXPORT void QCheckBox_ConnectStateChanged(void* widget, void (*callback)(void*, int), void* userData) {
        QObject::connect((QCheckBox*)widget, &QCheckBox::stateChanged, [callback, userData](int state) {
            callback(userData, state);
        });
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

    EXPORT void QWidget_SetEnabled(void* widget, bool enabled) {
        ((QWidget*)widget)->setEnabled(enabled);
    }
    
    EXPORT void* QLineEdit_Create(void* parent, const char* text) {
        QLineEdit* widget = new QLineEdit(QString::fromUtf8(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QLineEdit_SetText(void* lineEdit, const char* text) {
        ((QLineEdit*)lineEdit)->setText(QString::fromUtf8(text));
    }

    EXPORT void QLineEdit_GetText_Invoke(void* label, ReadQStringCallback cb, void* userData) {
        QString s = ((QLineEdit*)label)->text();
        cb((const void*)s.constData(), s.size(), userData);
    }

    // Resize and Move event support
    class ResizeEventFilter : public QObject {
    private:
        void (*resizeCallback)(void*, int, int);
        void (*moveCallback)(void*, int, int);
        void* userData;

    public:
        ResizeEventFilter(void (*resizeCb)(void*, int, int), void (*moveCb)(void*, int, int), void* data)
            : resizeCallback(resizeCb), moveCallback(moveCb), userData(data) {}

    protected:
        bool eventFilter(QObject* obj, QEvent* event) override {
            if (event->type() == QEvent::Resize && resizeCallback) {
                QResizeEvent* resizeEvent = static_cast<QResizeEvent*>(event);
                resizeCallback(userData, resizeEvent->size().width(), resizeEvent->size().height());
            }
            else if (event->type() == QEvent::Move && moveCallback) {
                QMoveEvent* moveEvent = static_cast<QMoveEvent*>(event);
                moveCallback(userData, moveEvent->pos().x(), moveEvent->pos().y());
            }
            return QObject::eventFilter(obj, event);
        }
    };

    EXPORT void QWidget_ConnectResize(void* widget, void (*resizeCallback)(void*, int, int), void (*moveCallback)(void*, int, int), void* userData) {
        QWidget* w = static_cast<QWidget*>(widget);
        ResizeEventFilter* filter = new ResizeEventFilter(resizeCallback, moveCallback, userData);
        w->installEventFilter(filter);
    }
}

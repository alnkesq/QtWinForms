#include <QApplication>
#include <QCoreApplication>
#include <QWidget>
#include <QPushButton>
#include <QLabel>
#include <QCheckBox>
#include <QLineEdit>
#include <QPlainTextEdit>
#include <QGroupBox>
#include <QTabWidget>
#include <QMessageBox>
#include <QEvent>
#include <QResizeEvent>
#include <QMoveEvent>
#include <QCloseEvent>
#include <QKeyEvent>
#include <QProgressBar>
#include <QRadioButton>
#include <QMenuBar>
#include <QMenu>
#include <QAction>
#include <QComboBox>
#include <QFileDialog>
#include <QColorDialog>
#include <QFontDialog>
#include <QDoubleSpinBox>
#include <QSlider>
#include <QListWidget>
#include <QDateTimeEdit>
#include <QDateTime>
#include <QDateTimeEdit>
#include <QDateTime>
#include <QPixmap>
#include <QBuffer>
#include <QPainter>
#include <QToolBar>
#include <QTreeWidget>
#include <QTreeWidgetItem>
#include <QImageReader>
#include <iostream>
using namespace std;
#ifdef _WIN32
#define EXPORT __declspec(dllexport)
#else
#define EXPORT
#endif

extern "C" {

    typedef void (*ReadQStringCallback)(const void* dataUtf16, int length, void* userData);

    // Custom event for executing callbacks on the main thread
    class CallbackEvent : public QEvent {
    public:
        static const QEvent::Type Type = static_cast<QEvent::Type>(QEvent::User + 1);
        typedef void (*Callback)(void*);
        Callback callback;
        void* data;

        CallbackEvent(Callback cb, void* d) : QEvent(Type), callback(cb), data(d) {}
    };

    // Global object to receive events
    class EventDispatcher : public QObject {
    protected:
        bool event(QEvent* e) override {
            if (e->type() == CallbackEvent::Type) {
                CallbackEvent* ce = static_cast<CallbackEvent*>(e);
                if (ce->callback) {
                    ce->callback(ce->data);
                }
                return true;
            }
            return QObject::event(e);
        }
    };

    static EventDispatcher* g_dispatcher = nullptr;

    EXPORT void* QApplication_Create() {
        static int argc = 1;
        static char* argv[] = { (char*)"TestApp", nullptr };
        QApplication* app = new QApplication(argc, argv);
        
        if (!g_dispatcher) {
            g_dispatcher = new EventDispatcher();
        }
        
        return app;
    }
    
    EXPORT void QApplication_InvokeOnMainThread(void (*callback)(void*), void* userData) {
        if (g_dispatcher) {
            QCoreApplication::postEvent(g_dispatcher, new CallbackEvent(callback, userData));
        }
    }
    
    EXPORT void QApplication_Run() {
        QApplication::exec();
    }
    
    EXPORT void* QWidget_Create(void* parent) {
        return new QWidget((QWidget*)parent);
    }

    EXPORT void QWidget_Destroy(void* widget) {
        delete (QWidget*)widget;
    }
    
    EXPORT void QWidget_Show(void* widget) {
        ((QWidget*)widget)->show();
    }

    EXPORT void QWidget_Hide(void* widget) {
        ((QWidget*)widget)->hide();
    }

    EXPORT bool QWidget_IsVisible(void* widget) {
        return ((QWidget*)widget)->isVisible();
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

    EXPORT void QWidget_SetIcon(void* widget, void* icon) {
        QWidget* w = (QWidget*)widget;
        QIcon* qIcon = (QIcon*)icon;
        if (qIcon != nullptr) {
            w->setWindowIcon(*qIcon);
        } else {
            w->setWindowIcon(QIcon());
        }
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

    EXPORT void* QRadioButton_Create(void* parent, const char* text) {
        QRadioButton* widget = new QRadioButton(QString::fromUtf8(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QRadioButton_SetText(void* widget, const char* text) {
        ((QRadioButton*)widget)->setText(QString::fromUtf8(text));
    }
    
    EXPORT void QRadioButton_SetChecked(void* widget, bool isChecked) {
        ((QRadioButton*)widget)->setChecked(isChecked);
    }
    
    EXPORT void QRadioButton_ConnectToggled(void* widget, void (*callback)(void*, bool), void* userData) {
        QObject::connect((QRadioButton*)widget, &QRadioButton::toggled, [callback, userData](bool checked) {
            callback(userData, checked);
        });
    }
    
    EXPORT void QWidget_SetBackColor(void* widget, unsigned char r, unsigned char g, unsigned char b, unsigned char a) {
        QWidget* w = (QWidget*)widget;
        QPalette palette = w->palette();
        palette.setColor(QPalette::Window, QColor(r, g, b, a));
        w->setAutoFillBackground(true);
        w->setPalette(palette);
    }
    
    EXPORT void QWidget_SetForeColor(void* widget, unsigned char r, unsigned char g, unsigned char b, unsigned char a) {
        QWidget* w = (QWidget*)widget;
        QPalette palette = w->palette();
        palette.setColor(QPalette::WindowText, QColor(r, g, b, a));
        w->setPalette(palette);
    }

    EXPORT void QWidget_SetEnabled(void* widget, bool enabled) {
        ((QWidget*)widget)->setEnabled(enabled);
    }

    EXPORT void QWidget_SetFont(void* widget, const char* family, float size, bool bold, bool italic, bool underline, bool strikeout) {
        QFont font(QString::fromUtf8(family));
        font.setPointSizeF(size);
        font.setBold(bold);
        font.setItalic(italic);
        font.setUnderline(underline);
        font.setStrikeOut(strikeout);
        ((QWidget*)widget)->setFont(font);
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

    EXPORT void QLineEdit_SetEchoMode(void* lineEdit, int mode) {
        // 0 = Normal, 1 = NoEcho, 2 = Password, 3 = PasswordEchoOnEdit
        ((QLineEdit*)lineEdit)->setEchoMode((QLineEdit::EchoMode)mode);
    }

    EXPORT void* QPlainTextEdit_Create(void* parent, const char* text) {
        QPlainTextEdit* widget = new QPlainTextEdit(QString::fromUtf8(text), (QWidget*)parent);
        return widget;
    }

    EXPORT void QPlainTextEdit_SetText(void* widget, const char* text) {
        ((QPlainTextEdit*)widget)->setPlainText(QString::fromUtf8(text));
    }

    EXPORT void QPlainTextEdit_GetText_Invoke(void* widget, ReadQStringCallback cb, void* userData) {
        QString s = ((QPlainTextEdit*)widget)->toPlainText();
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
                // For top-level windows, QMoveEvent::pos() returns the client area position,
                // but QWidget::move() expects the frame position (including title bar).
                // Use frameGeometry() to get the correct position including the window frame.
                QWidget* widget = qobject_cast<QWidget*>(obj);
                if (widget) {
                    QPoint framePos = widget->frameGeometry().topLeft();
                    moveCallback(userData, framePos.x(), framePos.y());
                }
            }
            return QObject::eventFilter(obj, event);
        }
    };

    EXPORT void QWidget_ConnectResize(void* widget, void (*resizeCallback)(void*, int, int), void (*moveCallback)(void*, int, int), void* userData) {
        QWidget* w = static_cast<QWidget*>(widget);
        ResizeEventFilter* filter = new ResizeEventFilter(resizeCallback, moveCallback, userData);
        w->installEventFilter(filter);
    }

    // Close event support
    class CloseEventFilter : public QObject {
    private:
        int (*closeCallback)(void*);
        void (*closedCallback)(void*);
        void* userData;

    public:
        CloseEventFilter(int (*closeCb)(void*), void (*closedCb)(void*), void* data)
            : closeCallback(closeCb), closedCallback(closedCb), userData(data) {}

    protected:
        bool eventFilter(QObject* obj, QEvent* event) override {
            if (event->type() == QEvent::Close && closeCallback) {
                QCloseEvent* closeEvent = static_cast<QCloseEvent*>(event);
                // Call the callback and get whether to allow the close (1 = allow, 0 = cancel)
                int allowClose = closeCallback(userData);
                if (allowClose == 0) {
                    // Cancel the close event
                    closeEvent->ignore();
                    return true; // Event handled
                }
                // If close is allowed and we have a closed callback, invoke it
                if (allowClose == 1 && closedCallback) {
                    closedCallback(userData);
                }
            }
            return QObject::eventFilter(obj, event);
        }
    };


    EXPORT void QWidget_ConnectCloseEvent(void* widget, int (*closeCallback)(void*), void (*closedCallback)(void*), void* userData) {
        QWidget* w = static_cast<QWidget*>(widget);
        CloseEventFilter* filter = new CloseEventFilter(closeCallback, closedCallback, userData);
        w->installEventFilter(filter);
    }

    // Key event support
    class KeyEventFilter : public QObject {
    private:
        bool (*keyCallback)(void*, int, int); // key, modifiers. Returns true if handled/suppressed.
        void* userData;

    public:
        KeyEventFilter(bool (*cb)(void*, int, int), void* data)
            : keyCallback(cb), userData(data) {}

    protected:
        bool eventFilter(QObject* obj, QEvent* event) override {
            if (event->type() == QEvent::KeyPress) {
                QKeyEvent* keyEvent = static_cast<QKeyEvent*>(event);
                if (keyCallback) {
                    // Pass key and modifiers
                    bool handled = keyCallback(userData, keyEvent->key(), (int)keyEvent->modifiers());
                    if (handled) {
                        return true; // Stop propagation (SuppressKeyPress)
                    }
                }
            }
            return QObject::eventFilter(obj, event);
        }
    };

    EXPORT void QWidget_ConnectKeyEvent(void* widget, bool (*callback)(void*, int, int), void* userData) {
        QWidget* w = static_cast<QWidget*>(widget);
        KeyEventFilter* filter = new KeyEventFilter(callback, userData);
        w->installEventFilter(filter);
    }


    EXPORT void QWidget_Close(void* widget) {
        QWidget* w = static_cast<QWidget*>(widget);
        w->close();
    }

    EXPORT void* QGroupBox_Create(void* parent, const char* title) {
        QGroupBox* widget = new QGroupBox(QString::fromUtf8(title), (QWidget*)parent);
        return widget;
    }

    EXPORT void QGroupBox_SetTitle(void* groupBox, const char* title) {
        ((QGroupBox*)groupBox)->setTitle(QString::fromUtf8(title));
    }

    EXPORT void* QTabWidget_Create(void* parent) {
        QTabWidget* widget = new QTabWidget((QWidget*)parent);
        return widget;
    }

    EXPORT void QTabWidget_AddTab(void* tabWidget, void* page, const char* label) {
        QTabWidget* tw = (QTabWidget*)tabWidget;
        QWidget* pageWidget = (QWidget*)page;
        tw->addTab(pageWidget, QString::fromUtf8(label));
    }

    EXPORT void QTabWidget_RemoveTab(void* tabWidget, int index) {
        QTabWidget* tw = (QTabWidget*)tabWidget;
        tw->removeTab(index);
    }

    EXPORT int QTabWidget_GetCurrentIndex(void* tabWidget) {
        QTabWidget* tw = (QTabWidget*)tabWidget;
        return tw->currentIndex();
    }

    EXPORT void QTabWidget_SetCurrentIndex(void* tabWidget, int index) {
        QTabWidget* tw = (QTabWidget*)tabWidget;
        tw->setCurrentIndex(index);
    }

    EXPORT void QTabWidget_ConnectCurrentChanged(void* tabWidget, void (*callback)(void*, int), void* userData) {
        QObject::connect((QTabWidget*)tabWidget, &QTabWidget::currentChanged, [callback, userData](int index) {
            callback(userData, index);
        });
    }

    EXPORT int QMessageBox_Show(void* parent, const char* text, const char* caption, int buttons, int icon, int defaultButton, int options) {
        QWidget* parentWidget = (QWidget*)parent;
        
        // Create message box
        QMessageBox msgBox(parentWidget);
        msgBox.setText(QString::fromUtf8(text));
        msgBox.setWindowTitle(QString::fromUtf8(caption));
        
        // Remove minimize button (unnecessary for message boxes)
        // Use explicit flags: Dialog with title bar and close button only
        //msgBox.setWindowFlags(Qt::Dialog | Qt::WindowTitleHint | Qt::WindowCloseButtonHint | Qt::MSWindowsFixedSizeDialogHint);
        
        // Set icon based on MessageBoxIcon enum
        // None=0, Error/Hand/Stop=16, Question=32, Exclamation/Warning=48, Information/Asterisk=64
        switch (icon) {
            case 16: // Error, Hand, Stop
                msgBox.setIcon(QMessageBox::Critical);
                break;
            case 32: // Question
                msgBox.setIcon(QMessageBox::Question);
                break;
            case 48: // Exclamation, Warning
                msgBox.setIcon(QMessageBox::Warning);
                break;
            case 64: // Information, Asterisk
                msgBox.setIcon(QMessageBox::Information);
                break;
            default: // None
                msgBox.setIcon(QMessageBox::NoIcon);
                break;
        }
        
        // Set buttons based on MessageBoxButtons enum
        // OK=0, OKCancel=1, AbortRetryIgnore=2, YesNoCancel=3, YesNo=4, RetryCancel=5
        QMessageBox::StandardButtons standardButtons;
        switch (buttons) {
            case 0: // OK
                standardButtons = QMessageBox::Ok;
                break;
            case 1: // OKCancel
                standardButtons = QMessageBox::Ok | QMessageBox::Cancel;
                break;
            case 2: // AbortRetryIgnore
                standardButtons = QMessageBox::Abort | QMessageBox::Retry | QMessageBox::Ignore;
                break;
            case 3: // YesNoCancel
                standardButtons = QMessageBox::Yes | QMessageBox::No | QMessageBox::Cancel;
                break;
            case 4: // YesNo
                standardButtons = QMessageBox::Yes | QMessageBox::No;
                break;
            case 5: // RetryCancel
                standardButtons = QMessageBox::Retry | QMessageBox::Cancel;
                break;
            default:
                standardButtons = QMessageBox::Ok;
                break;
        }
        msgBox.setStandardButtons(standardButtons);
        
        // Set default button based on MessageBoxDefaultButton enum
        // Button1=0, Button2=256, Button3=512
        QMessageBox::StandardButton defButton = QMessageBox::NoButton;
        if (defaultButton == 256) { // Button2
            // Determine which button is the second one based on button configuration
            switch (buttons) {
                case 1: defButton = QMessageBox::Cancel; break; // OKCancel
                case 2: defButton = QMessageBox::Retry; break; // AbortRetryIgnore
                case 3: defButton = QMessageBox::No; break; // YesNoCancel
                case 4: defButton = QMessageBox::No; break; // YesNo
                case 5: defButton = QMessageBox::Cancel; break; // RetryCancel
            }
        } else if (defaultButton == 512) { // Button3
            // Determine which button is the third one based on button configuration
            switch (buttons) {
                case 2: defButton = QMessageBox::Ignore; break; // AbortRetryIgnore
                case 3: defButton = QMessageBox::Cancel; break; // YesNoCancel
            }
        } else { // Button1 (default)
            switch (buttons) {
                case 0: defButton = QMessageBox::Ok; break;
                case 1: defButton = QMessageBox::Ok; break;
                case 2: defButton = QMessageBox::Abort; break;
                case 3: defButton = QMessageBox::Yes; break;
                case 4: defButton = QMessageBox::Yes; break;
                case 5: defButton = QMessageBox::Retry; break;
            }
        }
        if (defButton != QMessageBox::NoButton) {
            msgBox.setDefaultButton(defButton);
        }
        msgBox.setWindowModality(Qt::ApplicationModal);
        msgBox.setWindowFlags(
            Qt::Dialog |
            Qt::CustomizeWindowHint |
            Qt::MSWindowsFixedSizeDialogHint |
            Qt::WindowCloseButtonHint
        );
        // Show the message box and get result
        int result = msgBox.exec();
        
        // Map Qt result to DialogResult enum
        // None=0, OK=1, Cancel=2, Abort=3, Retry=4, Ignore=5, Yes=6, No=7
        switch (result) {
            case QMessageBox::Ok:
                return 1; // DialogResult.OK
            case QMessageBox::Cancel:
                return 2; // DialogResult.Cancel
            case QMessageBox::Abort:
                return 3; // DialogResult.Abort
            case QMessageBox::Retry:
                return 4; // DialogResult.Retry
            case QMessageBox::Ignore:
                return 5; // DialogResult.Ignore
            case QMessageBox::Yes:
                return 6; // DialogResult.Yes
            case QMessageBox::No:
                return 7; // DialogResult.No
            default:
                return 0; // DialogResult.None
        }
    }

    EXPORT void QWidget_SetWindowState(void* widget, int state) {
        QWidget* w = (QWidget*)widget;
        Qt::WindowStates qtState = Qt::WindowNoState;
        if (state == 1) { // Minimized
            qtState = Qt::WindowMinimized;
        } else if (state == 2) { // Maximized
            qtState = Qt::WindowMaximized;
        }
        w->setWindowState(qtState);
    }

    EXPORT int QWidget_GetWindowState(void* widget) {
        QWidget* w = (QWidget*)widget;
        Qt::WindowStates qtState = w->windowState();
        if (qtState & Qt::WindowMinimized) {
            return 1;
        } else if (qtState & Qt::WindowMaximized) {
            return 2;
        }
        return 0;
    }

    EXPORT void QWidget_SetFormProperties(void* widget, bool minimizeBox, bool maximizeBox, bool showInTaskbar, bool showIcon, bool topMost, int formBorderStyle) {
        QWidget* w = (QWidget*)widget;
        
        Qt::WindowFlags flags = w->windowFlags();
        
        // Clear flags we are about to manage
        flags &= ~(Qt::WindowMinimizeButtonHint | Qt::WindowMaximizeButtonHint | Qt::WindowCloseButtonHint | Qt::WindowSystemMenuHint | Qt::WindowStaysOnTopHint | Qt::Tool | Qt::Window | Qt::Dialog | Qt::FramelessWindowHint | Qt::MSWindowsFixedSizeDialogHint | Qt::CustomizeWindowHint);
        
        // FormBorderStyle: 
        // None = 0
        // FixedSingle = 1
        // Fixed3D = 2
        // FixedDialog = 3
        // Sizable = 4
        // FixedToolWindow = 5
        // SizableToolWindow = 6
        
        bool isFixed = (formBorderStyle == 1 || formBorderStyle == 2 || formBorderStyle == 3 || formBorderStyle == 5);
        bool isToolWindow = (formBorderStyle == 5 || formBorderStyle == 6);
        bool isFrameless = (formBorderStyle == 0);
        
        // Set Window Type
        if (isFrameless) {
            flags |= Qt::FramelessWindowHint;
        } else {
            if (isToolWindow) {
                flags |= Qt::Tool;
            } else if (!showInTaskbar) {
                flags |= Qt::Tool; // If not in taskbar, often Tool works best, or we can use Window with specific attributes
            } else {
                flags |= Qt::Window;
            }
        }
        
        // Basic hints
        if (!isFrameless) {
            flags |= Qt::CustomizeWindowHint;
            flags |= Qt::WindowTitleHint;
            flags |= Qt::WindowCloseButtonHint; 
            
            if (minimizeBox) flags |= Qt::WindowMinimizeButtonHint;
            if (maximizeBox) flags |= Qt::WindowMaximizeButtonHint;
            
            if (showIcon) {
                flags |= Qt::WindowSystemMenuHint;
            }
            
            if (topMost) {
                flags |= Qt::WindowStaysOnTopHint;
            }
            
            if (isFixed) {
                flags |= Qt::MSWindowsFixedSizeDialogHint;
                // Also need to ensure the widget cannot be resized by user
                // Qt::MSWindowsFixedSizeDialogHint does this on Windows for the frame
                // We also need to set size constraint if we want to be strict, but for now rely on the hint
                // If we use SetFixedSize on layout it might be too restrictive for programmatic resize
                // But MSWindowsFixedSizeDialogHint is usually enough for the window manager
            }
        }
        
        bool wasVisible = w->isVisible();
        w->setWindowFlags(flags);
        // If fixed, we might want to enforce it on the widget level too to be safe, 
        // but Form.Size setter handles the size. 
        // The hint mainly affects the border style and user interaction.
        
        if (wasVisible) {
            w->show();
        }
    }

    EXPORT void* QProgressBar_Create(void* parent) {
        QProgressBar* widget = new QProgressBar((QWidget*)parent);
        return widget;
    }

    EXPORT void QProgressBar_SetRange(void* progressBar, int min, int max) {
        ((QProgressBar*)progressBar)->setRange(min, max);
    }

    EXPORT void QProgressBar_SetValue(void* progressBar, int value) {
        ((QProgressBar*)progressBar)->setValue(value);
    }

    EXPORT void* QMenuBar_Create(void* parent) {
        QMenuBar* menuBar = new QMenuBar((QWidget*)parent);
        return menuBar;
    }

    EXPORT void QMenuBar_AddAction(void* menuBar, void* action) {
        ((QMenuBar*)menuBar)->addAction((QAction*)action);
    }

    EXPORT void* QAction_Create(const char* text) {
        QAction* action = new QAction(QString::fromUtf8(text));
        return action;
    }

    EXPORT void QAction_SetText(void* action, const char* text) {
        ((QAction*)action)->setText(QString::fromUtf8(text));
    }

    EXPORT void QAction_ConnectTriggered(void* action, void (*callback)(void*), void* userData) {
        QObject::connect((QAction*)action, &QAction::triggered, [callback, userData]() {
            callback(userData);
        });
    }

    EXPORT void* QAction_CreateSeparator() {
        QAction* separator = new QAction();
        separator->setSeparator(true);
        return separator;
    }

    EXPORT void QAction_SetIcon(void* action, void* icon) {
        QAction* qAction = (QAction*)action;
        QIcon* qIcon = (QIcon*)icon;
        if (qIcon != nullptr) {
            qAction->setIcon(*qIcon);
        } else {
            qAction->setIcon(QIcon());
        }
    }

    EXPORT void QAction_SetToolTip(void* action, const char* toolTip) {
        ((QAction*)action)->setToolTip(QString::fromUtf8(toolTip));
    }

    EXPORT void QAction_SetVisible(void* action, bool visible) {
        ((QAction*)action)->setVisible(visible);
    }



    EXPORT void QWidget_SetMenuBar(void* widget, void* menuBar) {
        QWidget* w = (QWidget*)widget;
        // Only QMainWindow has a setMenuBar method, but we can use QWidget::layout
        // For simplicity, we'll cast to QMainWindow if needed
        // Actually, in Qt, only QMainWindow supports menuBar properly
        // For a regular QWidget acting as a window, we need to use a layout
        // But for Forms, we should use QMainWindow instead
        // For now, let's assume the widget is a top-level window and add the menubar
        QMenuBar* mb = (QMenuBar*)menuBar;
        mb->setParent(w);
        // Position the menu bar at the top
        mb->setGeometry(0, 0, w->width(), mb->sizeHint().height());
    }

    EXPORT void* QMenu_Create(const char* title) {
        QMenu* menu = new QMenu(QString::fromUtf8(title));
        return menu;
    }

    EXPORT void QMenu_AddAction(void* menu, void* action) {
        ((QMenu*)menu)->addAction((QAction*)action);
    }

    EXPORT void QMenu_AddMenu(void* menu, void* submenu) {
        ((QMenu*)menu)->addMenu((QMenu*)submenu);
    }

    EXPORT void QMenuBar_AddMenu(void* menuBar, void* menu) {
        ((QMenuBar*)menuBar)->addMenu((QMenu*)menu);
    }

    EXPORT void QAction_SetMenu(void* action, void* menu) {
        ((QAction*)action)->setMenu((QMenu*)menu);
    }


    EXPORT void* QLinkLabel_Create(void* parent, const char* text) {
        QLabel* widget = new QLabel(QString::fromUtf8(text), (QWidget*)parent);
        widget->setTextInteractionFlags(Qt::TextBrowserInteraction);
        widget->setOpenExternalLinks(false); 
        return widget;
    }

    EXPORT void QLinkLabel_ConnectLinkClicked(void* widget, void (*callback)(void*, const char*), void* userData) {
        QObject::connect((QLabel*)widget, &QLabel::linkActivated, [callback, userData](const QString &link) {
            callback(userData, link.toUtf8().constData());
        });
    }

    EXPORT void* QComboBox_Create(void* parent) {
        QComboBox* widget = new QComboBox((QWidget*)parent);
        return widget;
    }

    EXPORT void QComboBox_AddItem(void* comboBox, const char* text) {
        ((QComboBox*)comboBox)->addItem(QString::fromUtf8(text));
    }

    EXPORT void QComboBox_SetEditable(void* comboBox, bool editable) {
        ((QComboBox*)comboBox)->setEditable(editable);
    }

    EXPORT int QComboBox_GetSelectedIndex(void* comboBox) {
        return ((QComboBox*)comboBox)->currentIndex();
    }

    EXPORT void QComboBox_SetSelectedIndex(void* comboBox, int index) {
        ((QComboBox*)comboBox)->setCurrentIndex(index);
    }

    EXPORT void QComboBox_ConnectSelectedIndexChanged(void* comboBox, void (*callback)(void*, int), void* userData) {
        QObject::connect((QComboBox*)comboBox, QOverload<int>::of(&QComboBox::currentIndexChanged), [callback, userData](int index) {
            callback(userData, index);
        });
    }

    EXPORT void QComboBox_Clear(void* comboBox) {
        ((QComboBox*)comboBox)->clear();
    }

    EXPORT void QComboBox_InsertItem(void* comboBox, int index, const char* text) {
        ((QComboBox*)comboBox)->insertItem(index, QString::fromUtf8(text));
    }

    EXPORT void QComboBox_RemoveItem(void* comboBox, int index) {
        ((QComboBox*)comboBox)->removeItem(index);
    }

    EXPORT void QComboBox_SetText(void* comboBox, const char* text) {
        ((QComboBox*)comboBox)->setCurrentText(QString::fromUtf8(text));
    }

    EXPORT void QComboBox_GetText_Invoke(void* comboBox, ReadQStringCallback cb, void* userData) {
        QString s = ((QComboBox*)comboBox)->currentText();
        cb((const void*)s.constData(), s.size(), userData);
    }

    EXPORT void QComboBox_ConnectCurrentTextChanged(void* comboBox, ReadQStringCallback callback, void* userData) {
        QObject::connect((QComboBox*)comboBox, &QComboBox::currentTextChanged, [callback, userData](const QString &text) {
            callback((const void*)text.constData(), text.size(), userData);
        });
    }

    EXPORT void QFileDialog_GetExistingDirectory(void* parent, const char* initialDirectory, const char* title, bool showNewFolderButton, ReadQStringCallback callback, void* userData) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QFileDialog::Options options = QFileDialog::ShowDirsOnly;
        if (!showNewFolderButton) {
            options |= QFileDialog::ReadOnly;
        }

        QString dir = QFileDialog::getExistingDirectory(parentWidget, QString::fromUtf8(title), QString::fromUtf8(initialDirectory), options);
        
        if (!dir.isEmpty()) {
             callback((const void*)dir.constData(), dir.size(), userData);
        }
    }

    EXPORT void QFileDialog_GetOpenFileName(void* parent, const char* initialDirectory, const char* title, const char* filter, ReadQStringCallback callback, void* userData) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QString fileName = QFileDialog::getOpenFileName(parentWidget, QString::fromUtf8(title), QString::fromUtf8(initialDirectory), QString::fromUtf8(filter));
        
        if (!fileName.isEmpty()) {
             callback((const void*)fileName.constData(), fileName.size(), userData);
        }
    }

    EXPORT void QFileDialog_GetSaveFileName(void* parent, const char* initialDirectory, const char* title, const char* filter, ReadQStringCallback callback, void* userData) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QString fileName = QFileDialog::getSaveFileName(parentWidget, QString::fromUtf8(title), QString::fromUtf8(initialDirectory), QString::fromUtf8(filter));
        
        if (!fileName.isEmpty()) {
             callback((const void*)fileName.constData(), fileName.size(), userData);
        }
    }

    EXPORT int QColorDialog_GetColor(void* parent, int initialColor, bool showAlphaChannel) {
        QWidget* parentWidget = (QWidget*)parent;
        
        // Convert ARGB int to QColor
        // ARGB format: 0xAARRGGBB
        int a = (initialColor >> 24) & 0xFF;
        int r = (initialColor >> 16) & 0xFF;
        int g = (initialColor >> 8) & 0xFF;
        int b = initialColor & 0xFF;
        QColor initial(r, g, b, a);
        
        // Show color dialog
        QColorDialog::ColorDialogOptions options = QColorDialog::ColorDialogOptions();
        if (showAlphaChannel) {
            options |= QColorDialog::ShowAlphaChannel;
        }
        
        QColor color = QColorDialog::getColor(initial, parentWidget, QString(), options);
        
        // Check if user cancelled
        if (!color.isValid()) {
            return -1;
        }
        
        // Convert QColor back to ARGB int
        int resultArgb = (color.alpha() << 24) | (color.red() << 16) | (color.green() << 8) | color.blue();
        return resultArgb;
    }

    EXPORT void* QDoubleSpinBox_Create(void* parent) {
        QDoubleSpinBox* widget = new QDoubleSpinBox((QWidget*)parent);
        return widget;
    }

    EXPORT void QDoubleSpinBox_SetValue(void* spinBox, double value) {
        ((QDoubleSpinBox*)spinBox)->setValue(value);
    }

    EXPORT double QDoubleSpinBox_GetValue(void* spinBox) {
        return ((QDoubleSpinBox*)spinBox)->value();
    }

    EXPORT void QDoubleSpinBox_SetRange(void* spinBox, double min, double max) {
        ((QDoubleSpinBox*)spinBox)->setRange(min, max);
    }

    EXPORT void QDoubleSpinBox_SetSingleStep(void* spinBox, double step) {
        ((QDoubleSpinBox*)spinBox)->setSingleStep(step);
    }

    EXPORT void QDoubleSpinBox_ConnectValueChanged(void* spinBox, void (*callback)(void*, double), void* userData) {
        QObject::connect((QDoubleSpinBox*)spinBox, QOverload<double>::of(&QDoubleSpinBox::valueChanged), [callback, userData](double value) {
            callback(userData, value);
        });
    }

    EXPORT void* QSlider_Create(void* parent) {
        QSlider* widget = new QSlider(Qt::Horizontal, (QWidget*)parent);
        return widget;
    }

    EXPORT void QSlider_SetRange(void* slider, int min, int max) {
        ((QSlider*)slider)->setRange(min, max);
    }

    EXPORT void QSlider_SetValue(void* slider, int value) {
        ((QSlider*)slider)->setValue(value);
    }

    EXPORT void QSlider_ConnectValueChanged(void* slider, void (*callback)(void*, int), void* userData) {
        QObject::connect((QSlider*)slider, &QSlider::valueChanged, [callback, userData](int value) {
            callback(userData, value);
        });
    }

    EXPORT void* QListWidget_Create(void* parent) {
        QListWidget* widget = new QListWidget((QWidget*)parent);
        return widget;
    }

    EXPORT void QListWidget_AddItem(void* listWidget, const char* text) {
        ((QListWidget*)listWidget)->addItem(QString::fromUtf8(text));
    }

    EXPORT void QListWidget_Clear(void* listWidget) {
        ((QListWidget*)listWidget)->clear();
    }

    EXPORT void QListWidget_InsertItem(void* listWidget, int index, const char* text) {
        ((QListWidget*)listWidget)->insertItem(index, QString::fromUtf8(text));
    }

    EXPORT void QListWidget_RemoveItem(void* listWidget, int index) {
        QListWidgetItem* item = ((QListWidget*)listWidget)->takeItem(index);
        delete item;
    }

    EXPORT void QListWidget_SetSelectionMode(void* listWidget, int mode) {
        QListWidget* lw = (QListWidget*)listWidget;
        // Map WinForms SelectionMode to Qt SelectionMode
        // None = 0, One = 1, MultiSimple = 2, MultiExtended = 3
        switch (mode) {
            case 0: // None
                lw->setSelectionMode(QAbstractItemView::NoSelection);
                break;
            case 1: // One
                lw->setSelectionMode(QAbstractItemView::SingleSelection);
                break;
            case 2: // MultiSimple
                lw->setSelectionMode(QAbstractItemView::MultiSelection);
                break;
            case 3: // MultiExtended
                lw->setSelectionMode(QAbstractItemView::ExtendedSelection);
                break;
            default:
                lw->setSelectionMode(QAbstractItemView::SingleSelection);
                break;
        }
    }

    EXPORT int QListWidget_GetCurrentRow(void* listWidget) {
        return ((QListWidget*)listWidget)->currentRow();
    }

    EXPORT void QListWidget_SetCurrentRow(void* listWidget, int row) {
        ((QListWidget*)listWidget)->setCurrentRow(row);
    }

    EXPORT void QListWidget_GetSelectedRows(void* listWidget, void (*callback)(int*, int, void*), void* userData) {
        QListWidget* lw = (QListWidget*)listWidget;
        QList<QListWidgetItem*> selectedItems = lw->selectedItems();
        
        if (selectedItems.isEmpty()) {
            callback(nullptr, 0, userData);
        } else {
            QVector<int> rows;
            for (QListWidgetItem* item : selectedItems) {
                rows.append(lw->row(item));
            }
            callback(rows.data(), rows.size(), userData);
        }
    }

    EXPORT void QListWidget_ConnectCurrentRowChanged(void* listWidget, void (*callback)(void*), void* userData) {
        QObject::connect((QListWidget*)listWidget, &QListWidget::currentRowChanged, [callback, userData](int currentRow) {
            callback(userData);
        });
    }

    EXPORT void* QDateTimeEdit_Create(void* parent) {
        QDateTimeEdit* widget = new QDateTimeEdit((QWidget*)parent);
        return widget;
    }

    EXPORT void QDateTimeEdit_SetDateTime(void* dateTimeEdit, int year, int month, int day, int hour, int minute, int second) {
        QDate date(year, month, day);
        QTime time(hour, minute, second);
        QDateTime dt(date, time);
        ((QDateTimeEdit*)dateTimeEdit)->setDateTime(dt);
    }

    EXPORT void QDateTimeEdit_GetDateTime(void* dateTimeEdit, int* year, int* month, int* day, int* hour, int* minute, int* second) {
        QDateTime dt = ((QDateTimeEdit*)dateTimeEdit)->dateTime();
        QDate date = dt.date();
        QTime time = dt.time();
        *year = date.year();
        *month = date.month();
        *day = date.day();
        *hour = time.hour();
        *minute = time.minute();
        *second = time.second();
    }

    EXPORT void QDateTimeEdit_SetMinimumDateTime(void* dateTimeEdit, int year, int month, int day, int hour, int minute, int second) {
        QDate date(year, month, day);
        QTime time(hour, minute, second);
        QDateTime dt(date, time);
        ((QDateTimeEdit*)dateTimeEdit)->setMinimumDateTime(dt);
    }

    EXPORT void QDateTimeEdit_SetMaximumDateTime(void* dateTimeEdit, int year, int month, int day, int hour, int minute, int second) {
        QDate date(year, month, day);
        QTime time(hour, minute, second);
        QDateTime dt(date, time);
        ((QDateTimeEdit*)dateTimeEdit)->setMaximumDateTime(dt);
    }

    EXPORT void QDateTimeEdit_ConnectDateTimeChanged(void* dateTimeEdit, void (*callback)(void*, int, int, int, int, int, int), void* userData) {
        QObject::connect((QDateTimeEdit*)dateTimeEdit, &QDateTimeEdit::dateTimeChanged, [callback, userData](const QDateTime &dateTime) {
            QDate date = dateTime.date();
            QTime time = dateTime.time();
            callback(userData, date.year(), date.month(), date.day(), time.hour(), time.minute(), time.second());
        });
    }

    EXPORT bool QFontDialog_GetFont(
        void* parent,
        const char* initialFamily,
        float initialSize,
        bool initialBold,
        bool initialItalic,
        bool initialUnderline,
        bool initialStrikeout,
        char* outFamily,
        int outFamilyMaxLen,
        float* outSize,
        bool* outBold,
        bool* outItalic,
        bool* outUnderline,
        bool* outStrikeout
    ) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QFont initialFont(QString::fromUtf8(initialFamily));
        initialFont.setPointSizeF(initialSize);
        initialFont.setBold(initialBold);
        initialFont.setItalic(initialItalic);
        initialFont.setUnderline(initialUnderline);
        initialFont.setStrikeOut(initialStrikeout);
        
        bool ok;
        QFont font = QFontDialog::getFont(&ok, initialFont, parentWidget);
        
        if (ok) {
            QString family = font.family();
            QByteArray familyUtf8 = family.toUtf8();
            strncpy(outFamily, familyUtf8.constData(), outFamilyMaxLen - 1);
            outFamily[outFamilyMaxLen - 1] = '\0';
            
            *outSize = font.pointSizeF();
            // If point size is -1, try pixel size
            if (*outSize <= 0) {
                *outSize = font.pixelSize();
            }
            
            *outBold = font.bold();
            *outItalic = font.italic();
            *outUnderline = font.underline();
            *outStrikeout = font.strikeOut();
            return true;
        }
        
        return false;
    }

    // PictureBox implementation
    class QPictureBox : public QLabel {
    public:
        QPictureBox(QWidget* parent = nullptr) : QLabel(parent) {
            setMinimumSize(1, 1);
            // Enable mouse tracking if needed, but for now just display
        }

        int sizeMode = 0; // 0=Normal, 1=Stretch, 2=AutoSize, 3=Center, 4=Zoom
        QPixmap originalPixmap;

        void setOriginalPixmap(const QPixmap& pixmap) {
            originalPixmap = pixmap;
            updateDisplay();
        }

        void setMode(int mode) {
            sizeMode = mode;
            updateDisplay();
        }

    protected:
        void resizeEvent(QResizeEvent* event) override {
            QLabel::resizeEvent(event);
            if (sizeMode == 4) { // Zoom
                updateDisplay();
            }
        }

        void updateDisplay() {
            if (originalPixmap.isNull()) {
                this->clear();
                return;
            }

            setAlignment(Qt::AlignTop | Qt::AlignLeft);
            setScaledContents(false);

            switch (sizeMode) {
                case 0: // Normal
                    setPixmap(originalPixmap);
                    break;
                case 1: // StretchImage
                    setScaledContents(true);
                    setPixmap(originalPixmap);
                    break;
                case 2: // AutoSize
                    setPixmap(originalPixmap);
                    adjustSize();
                    break;
                case 3: // CenterImage
                    setAlignment(Qt::AlignCenter);
                    setPixmap(originalPixmap);
                    break;
                case 4: // Zoom
                    // Scale keeping aspect ratio
                    if (width() > 0 && height() > 0) {
                        setPixmap(originalPixmap.scaled(size(), Qt::KeepAspectRatio, Qt::SmoothTransformation));
                        setAlignment(Qt::AlignCenter);
                    }
                    break;
            }
        }
    };

    EXPORT void* QPictureBox_Create(void* parent) {
        QPictureBox* widget = new QPictureBox((QWidget*)parent);
        return widget;
    }

    EXPORT void QPictureBox_SetImage(void* pictureBox, void* pixmap) {
        QPictureBox* pb = (QPictureBox*)pictureBox;
        QPixmap* qPixmap = (QPixmap*)pixmap;
        if (qPixmap == nullptr) {
            pb->setOriginalPixmap(QPixmap());
        } else {
            pb->setOriginalPixmap(*qPixmap);
        }
    }

    EXPORT void* QPixmap_CreateFromData(const unsigned char* data, int length) {
        QPixmap* pixmap = new QPixmap();
        pixmap->loadFromData(data, length);
        return pixmap;
    }

    EXPORT void QPixmap_Destroy(void* pixmap) {
        if (pixmap != nullptr) {
            delete (QPixmap*)pixmap;
        }
    }

    //EXPORT void QPictureBox_SetImageLocation(void* pictureBox, const char* path) {
    //    QPictureBox* pb = (QPictureBox*)pictureBox;
    //    QPixmap pixmap(QString::fromUtf8(path));
    //    pb->setOriginalPixmap(pixmap);
    //}

    EXPORT void QPictureBox_SetSizeMode(void* pictureBox, int mode) {
        QPictureBox* pb = (QPictureBox*)pictureBox;
        pb->setMode(mode);
    }

    EXPORT void* QToolBar_Create(void* parent) {
        QToolBar* toolBar = new QToolBar((QWidget*)parent);
        return toolBar;
    }

    EXPORT void QToolBar_AddAction(void* toolBar, void* action) {
        ((QToolBar*)toolBar)->addAction((QAction*)action);
    }

    EXPORT void QToolBar_SetToolButtonStyle(void* toolBar, int style) {
        // Map from ToolStripItemDisplayStyle to Qt::ToolButtonStyle
        // None = 0, Text = 1, Image = 2, ImageAndText = 3
        Qt::ToolButtonStyle qtStyle;
        switch (style) {
            case 1: // Text
                qtStyle = Qt::ToolButtonTextOnly;
                break;
            case 2: // Image
                qtStyle = Qt::ToolButtonIconOnly;
                break;
            case 3: // ImageAndText
                qtStyle = Qt::ToolButtonTextBesideIcon;
                break;
            default: // None
                qtStyle = Qt::ToolButtonFollowStyle;
                break;
        }
        ((QToolBar*)toolBar)->setToolButtonStyle(qtStyle);
    }


    EXPORT void QWidget_SetContextMenuPolicy(void* widget, int policy) {
        ((QWidget*)widget)->setContextMenuPolicy((Qt::ContextMenuPolicy)policy);
    }

    EXPORT void QWidget_ConnectCustomContextMenuRequested(void* widget, void (*callback)(void*, int, int), void* userData) {
        QObject::connect((QWidget*)widget, &QWidget::customContextMenuRequested, [callback, userData](const QPoint &pos) {
            callback(userData, pos.x(), pos.y());
        });
    }

    EXPORT void QMenu_Popup(void* menu, int x, int y) {
        ((QMenu*)menu)->popup(QPoint(x, y));
    }

    EXPORT void QWidget_MapToGlobal(void* widget, int x, int y, int* rx, int* ry) {
        QPoint p = ((QWidget*)widget)->mapToGlobal(QPoint(x, y));
        *rx = p.x();
        *ry = p.y();
    }

    EXPORT void* QTreeWidget_Create(void* parent) {
        QTreeWidget* widget = new QTreeWidget((QWidget*)parent);
        widget->setHeaderHidden(true);
        return widget;
    }

    EXPORT void* QTreeWidget_AddTopLevelItem(void* treeWidget, const char* text) {
        QTreeWidget* tw = (QTreeWidget*)treeWidget;
        QTreeWidgetItem* item = new QTreeWidgetItem();
        item->setText(0, QString::fromUtf8(text));
        tw->addTopLevelItem(item);
        return item;
    }

    EXPORT void* QTreeWidgetItem_AddChild(void* parentItem, const char* text) {
        QTreeWidgetItem* parent = (QTreeWidgetItem*)parentItem;
        QTreeWidgetItem* child = new QTreeWidgetItem();
        child->setText(0, QString::fromUtf8(text));
        parent->addChild(child);
        return child;
    }

    EXPORT void QTreeWidgetItem_SetText(void* item, int column, const char* text) {
        ((QTreeWidgetItem*)item)->setText(column, QString::fromUtf8(text));
    }

    EXPORT void QTreeWidgetItem_RemoveChild(void* parentItem, void* childItem) {
        QTreeWidgetItem* parent = (QTreeWidgetItem*)parentItem;
        QTreeWidgetItem* child = (QTreeWidgetItem*)childItem;
        parent->removeChild(child);
        delete child;
    }

    EXPORT void* QTreeWidget_GetCurrentItem(void* treeWidget) {
        return ((QTreeWidget*)treeWidget)->currentItem();
    }

    EXPORT void QTreeWidget_SetCurrentItem(void* treeWidget, void* item) {
        ((QTreeWidget*)treeWidget)->setCurrentItem((QTreeWidgetItem*)item);
    }

    EXPORT void QTreeWidget_ConnectItemSelectionChanged(void* treeWidget, void (*callback)(void*), void* userData) {
        QObject::connect((QTreeWidget*)treeWidget, &QTreeWidget::itemSelectionChanged, [callback, userData]() {
            callback(userData);
        });
    }

    EXPORT void QTreeWidgetItem_SetExpanded(void* item, bool expanded) {
        ((QTreeWidgetItem*)item)->setExpanded(expanded);
    }

    EXPORT void QTreeWidget_ConnectItemExpanded(void* treeWidget, void (*callback)(void*, void*), void* userData) {
        QObject::connect((QTreeWidget*)treeWidget, &QTreeWidget::itemExpanded, [callback, userData](QTreeWidgetItem* item) {
            callback(userData, item);
        });
    }

    static bool isIcoMagic(const void* ptr, size_t len)
    {
        if (len < 4) return false;
        const unsigned char* p = static_cast<const unsigned char*>(ptr);
        return p[0] == 0x00 && p[1] == 0x00 && p[2] == 0x01 && p[3] == 0x00;
    }
        
    EXPORT void* QIcon_CreateFromData(const unsigned char* ptr, int len) {
        QByteArray ba(reinterpret_cast<const char*>(ptr), len);

        if (!isIcoMagic(ptr, len)) {
            QPixmap px;
            px.loadFromData(ba);
            return new QIcon(px);
        }
            
        QBuffer buf(&ba);
        buf.open(QIODevice::ReadOnly);

        QImageReader reader(&buf, "ICO");

        QIcon* icon = new QIcon();
        do {
            QImage img = reader.read();
            if (!img.isNull())
                icon->addPixmap(QPixmap::fromImage(img));
        } while (reader.jumpToNextImage());
        return icon;
    }

    EXPORT void QIcon_Destroy(void* icon) {
        if (icon != nullptr) {
            delete (QIcon*)icon;
        }
    }

    EXPORT void QTreeWidgetItem_SetIcon(void* item, int column, void* icon) {
        QTreeWidgetItem* treeItem = (QTreeWidgetItem*)item;
        QIcon* qIcon = (QIcon*)icon;
        if (qIcon != nullptr) {
            treeItem->setIcon(column, *qIcon);
        } else {
            treeItem->setIcon(column, QIcon());
        }
    }

    }

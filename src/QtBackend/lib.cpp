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
#include <QSplitter>
#include <QTableWidget>
#include <QHeaderView>
#include <QMap>
#include <QEventLoop>
#include <QStyledItemDelegate>
#include <QClipboard>

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

    EXPORT void QWidget_Raise(void* widget) {
        ((QWidget*)widget)->raise();
    }

    EXPORT void QWidget_Lower(void* widget) {
        ((QWidget*)widget)->lower();
    }

    EXPORT void QWidget_StackUnder(void* widget, void* under) {
        ((QWidget*)widget)->stackUnder((QWidget*)under);
    }
    
    EXPORT void QWidget_SetTitle(void* widget, const char16_t* title) {
        ((QWidget*)widget)->setWindowTitle(QString::fromUtf16(title));
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
    
    EXPORT void* QPushButton_Create(void* parent, const char16_t* text) {
        QPushButton* widget = new QPushButton(QString::fromUtf16(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QPushButton_SetText(void* widget, const char16_t* text) {
        ((QPushButton*)widget)->setText(QString::fromUtf16(text));
    }
    
    EXPORT void QPushButton_ConnectClicked(void* widget, void (*callback)(void*), void* userData) {
        QObject::connect((QPushButton*)widget, &QPushButton::clicked, [callback, userData]() {
            callback(userData);
        });
    }
    
    EXPORT void* QLabel_Create(void* parent, const char16_t* text) {
        QLabel* widget = new QLabel(QString::fromUtf16(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QLabel_SetText(void* label, const char16_t* text) {
        ((QLabel*)label)->setText(QString::fromUtf16(text));
    }
    
    EXPORT void* QCheckBox_Create(void* parent, const char16_t* text) {
        QCheckBox* widget = new QCheckBox(QString::fromUtf16(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QCheckBox_SetText(void* widget, const char16_t* text) {
        ((QCheckBox*)widget)->setText(QString::fromUtf16(text));
    }
    
    EXPORT void QCheckBox_SetChecked(void* widget, bool isChecked) {
        ((QCheckBox*)widget)->setChecked(isChecked);
    }
    
    EXPORT void QCheckBox_ConnectStateChanged(void* widget, void (*callback)(void*, int), void* userData) {
        QObject::connect((QCheckBox*)widget, &QCheckBox::stateChanged, [callback, userData](int state) {
            callback(userData, state);
        });
    }

    EXPORT void* QRadioButton_Create(void* parent, const char16_t* text) {
        QRadioButton* widget = new QRadioButton(QString::fromUtf16(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QRadioButton_SetText(void* widget, const char16_t* text) {
        ((QRadioButton*)widget)->setText(QString::fromUtf16(text));
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

    EXPORT void QWidget_SetFont(void* widget, const char16_t* family, float size, bool bold, bool italic, bool underline, bool strikeout) {
        QFont font(QString::fromUtf16(family));
        font.setPointSizeF(size);
        font.setBold(bold);
        font.setItalic(italic);
        font.setUnderline(underline);
        font.setStrikeOut(strikeout);
        ((QWidget*)widget)->setFont(font);
    }
    
    EXPORT void* QLineEdit_Create(void* parent, const char16_t* text) {
        QLineEdit* widget = new QLineEdit(QString::fromUtf16(text), (QWidget*)parent);
        return widget;
    }
    
    EXPORT void QLineEdit_SetText(void* lineEdit, const char16_t* text) {
        ((QLineEdit*)lineEdit)->setText(QString::fromUtf16(text));
    }

    EXPORT void QLineEdit_GetText_Invoke(void* label, ReadQStringCallback cb, void* userData) {
        QString s = ((QLineEdit*)label)->text();
        cb((const void*)s.constData(), s.size(), userData);
    }

    EXPORT void QLineEdit_SetEchoMode(void* lineEdit, int mode) {
        // 0 = Normal, 1 = NoEcho, 2 = Password, 3 = PasswordEchoOnEdit
        ((QLineEdit*)lineEdit)->setEchoMode((QLineEdit::EchoMode)mode);
    }

    EXPORT void* QPlainTextEdit_Create(void* parent, const char16_t* text) {
        QPlainTextEdit* widget = new QPlainTextEdit(QString::fromUtf16(text), (QWidget*)parent);
        return widget;
    }

    EXPORT void QPlainTextEdit_SetText(void* widget, const char16_t* text) {
        ((QPlainTextEdit*)widget)->setPlainText(QString::fromUtf16(text));
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

    EXPORT void* QGroupBox_Create(void* parent, const char16_t* title) {
        QGroupBox* widget = new QGroupBox(QString::fromUtf16(title), (QWidget*)parent);
        return widget;
    }

    EXPORT void QGroupBox_SetTitle(void* groupBox, const char16_t* title) {
        ((QGroupBox*)groupBox)->setTitle(QString::fromUtf16(title));
    }

    EXPORT void* QTabWidget_Create(void* parent) {
        QTabWidget* widget = new QTabWidget((QWidget*)parent);
        return widget;
    }

    EXPORT void QTabWidget_AddTab(void* tabWidget, void* page, const char16_t* label) {
        QTabWidget* tw = (QTabWidget*)tabWidget;
        QWidget* pageWidget = (QWidget*)page;
        tw->addTab(pageWidget, QString::fromUtf16(label));
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

    EXPORT int QMessageBox_Show(void* parent, const char16_t* text, const char16_t* caption, int buttons, int icon, int defaultButton, int options) {
        QWidget* parentWidget = (QWidget*)parent;
        
        // Create message box
        QMessageBox msgBox(parentWidget);
        msgBox.setText(QString::fromUtf16(text));
        msgBox.setWindowTitle(QString::fromUtf16(caption));
        
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

    EXPORT void QWidget_SetWindowModality(void* widget, int modality) {
        QWidget* w = (QWidget*)widget;
        Qt::WindowModality mode = Qt::NonModal;
        if (modality == 1) mode = Qt::WindowModal;
        else if (modality == 2) mode = Qt::ApplicationModal;
        w->setWindowModality(mode);
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

    EXPORT void* QAction_Create(const char16_t* text) {
        QAction* action = new QAction(QString::fromUtf16(text));
        return action;
    }

    EXPORT void QAction_SetText(void* action, const char16_t* text) {
        ((QAction*)action)->setText(QString::fromUtf16(text));
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

    EXPORT void QAction_SetToolTip(void* action, const char16_t* toolTip) {
        ((QAction*)action)->setToolTip(QString::fromUtf16(toolTip));
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

    EXPORT void* QMenu_Create(const char16_t* title) {
        QMenu* menu = new QMenu(QString::fromUtf16(title));
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

    EXPORT void QMenu_ConnectAboutToShow(void* menu, void (*callback)(void*), void* userData) {
        QObject::connect((QMenu*)menu, &QMenu::aboutToShow, [callback, userData]() {
            callback(userData);
        });
    }

    EXPORT void QAction_SetMenu(void* action, void* menu) {
        ((QAction*)action)->setMenu((QMenu*)menu);
    }


    EXPORT void* QLinkLabel_Create(void* parent, const char16_t* text) {
        QLabel* widget = new QLabel(QString::fromUtf16(text), (QWidget*)parent);
        widget->setTextInteractionFlags(Qt::TextBrowserInteraction);
        widget->setOpenExternalLinks(false); 
        return widget;
    }

    EXPORT void QLinkLabel_ConnectLinkClicked(void* widget, void (*callback)(void*), void* userData) {
        QObject::connect((QLabel*)widget, &QLabel::linkActivated, [callback, userData](const QString &link) {
            callback(userData);
        });
    }

    EXPORT void* QComboBox_Create(void* parent) {
        QComboBox* widget = new QComboBox((QWidget*)parent);
        return widget;
    }

    EXPORT void QComboBox_AddItem(void* comboBox, const char16_t* text) {
        ((QComboBox*)comboBox)->addItem(QString::fromUtf16(text));
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

    EXPORT void QComboBox_InsertItem(void* comboBox, int index, const char16_t* text) {
        ((QComboBox*)comboBox)->insertItem(index, QString::fromUtf16(text));
    }

    EXPORT void QComboBox_RemoveItem(void* comboBox, int index) {
        ((QComboBox*)comboBox)->removeItem(index);
    }

    EXPORT void QComboBox_SetText(void* comboBox, const char16_t* text) {
        ((QComboBox*)comboBox)->setCurrentText(QString::fromUtf16(text));
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

    EXPORT void QFileDialog_GetExistingDirectory(void* parent, const char16_t* initialDirectory, const char16_t* title, bool showNewFolderButton, ReadQStringCallback callback, void* userData) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QFileDialog::Options options = QFileDialog::ShowDirsOnly;
        if (!showNewFolderButton) {
            options |= QFileDialog::ReadOnly;
        }

        QString dir = QFileDialog::getExistingDirectory(parentWidget, QString::fromUtf16(title), QString::fromUtf16(initialDirectory), options);
        
        if (!dir.isEmpty()) {
             callback((const void*)dir.constData(), dir.size(), userData);
        }
    }

    EXPORT void QFileDialog_GetOpenFileName(void* parent, const char16_t* initialDirectory, const char16_t* title, const char16_t* filter, ReadQStringCallback callback, void* userData) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QString fileName = QFileDialog::getOpenFileName(parentWidget, QString::fromUtf16(title), QString::fromUtf16(initialDirectory), QString::fromUtf16(filter));
        
        if (!fileName.isEmpty()) {
             callback((const void*)fileName.constData(), fileName.size(), userData);
        }
    }

    EXPORT void QFileDialog_GetSaveFileName(void* parent, const char16_t* initialDirectory, const char16_t* title, const char16_t* filter, ReadQStringCallback callback, void* userData) {
        QWidget* parentWidget = (QWidget*)parent;
        
        QString fileName = QFileDialog::getSaveFileName(parentWidget, QString::fromUtf16(title), QString::fromUtf16(initialDirectory), QString::fromUtf16(filter));
        
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

    EXPORT void QListWidget_AddItem(void* listWidget, const char16_t* text) {
        ((QListWidget*)listWidget)->addItem(QString::fromUtf16(text));
    }

    EXPORT void QListWidget_Clear(void* listWidget) {
        ((QListWidget*)listWidget)->clear();
    }

    EXPORT void QListWidget_InsertItem(void* listWidget, int index, const char16_t* text) {
        ((QListWidget*)listWidget)->insertItem(index, QString::fromUtf16(text));
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

    EXPORT void QListWidget_ConnectItemSelectionChanged(void* listWidget, void (*callback)(void*), void* userData) {
        QObject::connect((QListWidget*)listWidget, &QListWidget::itemSelectionChanged, [callback, userData]() {
            callback(userData);
        });
    }

    EXPORT void QListWidget_ConnectItemActivated(void* listWidget, void (*callback)(void*), void* userData) {
        QObject::connect((QListWidget*)listWidget, &QListWidget::itemActivated, [callback, userData](QListWidgetItem* item) {
            callback(userData);
        });
    }

    EXPORT void QListWidget_SetViewMode(void* listWidget, int mode) {
        QListWidget* lw = (QListWidget*)listWidget;
        // 0 = ListMode, 1 = IconMode
        if (mode == 1) {
            lw->setViewMode(QListView::IconMode);
        } else {
            lw->setViewMode(QListView::ListMode);
        }
    }

    EXPORT void QListWidgetItem_SetIcon(void* listWidget, int index, void* icon) {
        QListWidget* lw = (QListWidget*)listWidget;
        QListWidgetItem* item = lw->item(index);
        if (item != nullptr) {
            QIcon* qIcon = (QIcon*)icon;
            if (qIcon != nullptr) {
                item->setIcon(*qIcon);
            } else {
                item->setIcon(QIcon());
            }
        }
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
        const char16_t* initialFamily,
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
        
        QFont initialFont(QString::fromUtf16(initialFamily));
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

    //EXPORT void QPictureBox_SetImageLocation(void* pictureBox, const char16_t* path) {
    //    QPictureBox* pb = (QPictureBox*)pictureBox;
    //    QPixmap pixmap(QString::fromUtf16(path));
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

    EXPORT void* QTreeWidget_AddTopLevelItem(void* treeWidget, const char16_t* text) {
        QTreeWidget* tw = (QTreeWidget*)treeWidget;
        QTreeWidgetItem* item = new QTreeWidgetItem();
        item->setText(0, QString::fromUtf16(text));
        tw->addTopLevelItem(item);
        return item;
    }

    EXPORT void* QTreeWidgetItem_AddChild(void* parentItem, const char16_t* text) {
        QTreeWidgetItem* parent = (QTreeWidgetItem*)parentItem;
        QTreeWidgetItem* child = new QTreeWidgetItem();
        child->setText(0, QString::fromUtf16(text));
        parent->addChild(child);
        return child;
    }

    EXPORT void QTreeWidgetItem_SetText(void* item, int column, const char16_t* text) {
        ((QTreeWidgetItem*)item)->setText(column, QString::fromUtf16(text));
    }

    EXPORT void QTreeWidgetItem_RemoveChild(void* parentItem, void* childItem) {
        QTreeWidgetItem* parent = (QTreeWidgetItem*)parentItem;
        QTreeWidgetItem* child = (QTreeWidgetItem*)childItem;
        parent->removeChild(child);
        delete child;
    }

    EXPORT void QTreeWidget_RemoveTopLevelItem(void* treeWidget, void* item) {
        QTreeWidget* tw = (QTreeWidget*)treeWidget;
        QTreeWidgetItem* i = (QTreeWidgetItem*)item;
        int index = tw->indexOfTopLevelItem(i);
        if (index != -1) {
            tw->takeTopLevelItem(index);
            delete i;
        }
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

    EXPORT void QTreeWidget_ConnectItemActivated(void* treeWidget, void (*callback)(void*), void* userData) {
        QObject::connect((QTreeWidget*)treeWidget, &QTreeWidget::itemActivated, [callback, userData](QTreeWidgetItem* item, int column) {
            callback(userData);
        });
    }

    EXPORT void QTreeWidgetItem_SetExpanded(void* item, bool expanded) {
        ((QTreeWidgetItem*)item)->setExpanded(expanded);
    }

    EXPORT int QTreeWidgetItem_IsExpanded(void* item) {
        return ((QTreeWidgetItem*)item)->isExpanded();
    }

    EXPORT void QTreeWidget_ConnectItemExpanded(void* treeWidget, void (*callback)(void*, void*), void* userData) {
        QObject::connect((QTreeWidget*)treeWidget, &QTreeWidget::itemExpanded, [callback, userData](QTreeWidgetItem* item) {
            callback(userData, item);
        });
    }

    EXPORT void QTreeWidget_SetColumnCount(void* treeWidget, int count) {
        ((QTreeWidget*)treeWidget)->setColumnCount(count);
    }

    EXPORT void QTreeWidget_SetHeaderHidden(void* treeWidget, bool hidden) {
        ((QTreeWidget*)treeWidget)->setHeaderHidden(hidden);
    }

    EXPORT void QTreeWidget_SetHeaderLabels(void* treeWidget, const char16_t** labels, int count) {
        QStringList list;
        for (int i = 0; i < count; i++) {
            list << QString::fromUtf16(labels[i]);
        }
        ((QTreeWidget*)treeWidget)->setHeaderLabels(list);
    }

    EXPORT void QTreeWidget_Clear(void* treeWidget) {
        ((QTreeWidget*)treeWidget)->clear();
    }

    EXPORT void QTreeWidget_SetColumnWidth(void* treeWidget, int column, int width) {
        QTreeWidget* tw = (QTreeWidget*)treeWidget;
        tw->setColumnWidth(column, width);
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

    EXPORT void QTreeWidgetItem_SetToolTip(void* item, int column, const char16_t* toolTip) {
        ((QTreeWidgetItem*)item)->setToolTip(column, QString::fromUtf16(toolTip));
    }

    EXPORT void QTreeWidget_SetSelectionMode(void* treeWidget, int mode) {
        QTreeWidget* tw = (QTreeWidget*)treeWidget;
        // Map WinForms SelectionMode to Qt SelectionMode
        // None = 0, One = 1, MultiSimple = 2, MultiExtended = 3
        switch (mode) {
            case 0: // None
                tw->setSelectionMode(QAbstractItemView::NoSelection);
                break;
            case 1: // One
                tw->setSelectionMode(QAbstractItemView::SingleSelection);
                break;
            case 2: // MultiSimple
                tw->setSelectionMode(QAbstractItemView::MultiSelection);
                break;
            case 3: // MultiExtended
                tw->setSelectionMode(QAbstractItemView::ExtendedSelection);
                break;
            default:
                tw->setSelectionMode(QAbstractItemView::SingleSelection);
                break;
        }
    }

    EXPORT void QTreeWidget_GetSelectedRows(void* treeWidget, void (*callback)(int*, int, void*), void* userData) {
        QTreeWidget* tw = (QTreeWidget*)treeWidget;
        QList<QTreeWidgetItem*> selectedItems = tw->selectedItems();
        
        if (selectedItems.isEmpty()) {
            callback(nullptr, 0, userData);
        } else {
            QVector<int> rows;
            for (QTreeWidgetItem* item : selectedItems) {
                // Only consider top level items for ListView behavior
                int index = tw->indexOfTopLevelItem(item);
                if (index != -1) {
                    rows.append(index);
                }
            }
            callback(rows.data(), rows.size(), userData);
        }
    }

    EXPORT void QTreeWidgetItem_SetSelected(void* item, bool selected) {
        ((QTreeWidgetItem*)item)->setSelected(selected);
    }

    EXPORT bool QTreeWidgetItem_IsSelected(void* item) {
        return ((QTreeWidgetItem*)item)->isSelected();
    }

    EXPORT void QListWidget_SetItemSelected(void* listWidget, int row, bool selected) {
        QListWidget* lw = (QListWidget*)listWidget;
        QListWidgetItem* item = lw->item(row);
        if (item) {
            item->setSelected(selected);
        }
    }

    EXPORT bool QListWidget_IsItemSelected(void* listWidget, int row) {
        QListWidget* lw = (QListWidget*)listWidget;
        QListWidgetItem* item = lw->item(row);
        if (item) {
            return item->isSelected();
        }
        return false;
    }

    EXPORT void* QSplitter_Create(void* parent, int orientation) {
        QSplitter* splitter = new QSplitter((Qt::Orientation)orientation, (QWidget*)parent);
        return splitter;
    }

    EXPORT void QSplitter_SetOrientation(void* splitter, int orientation) {
        ((QSplitter*)splitter)->setOrientation((Qt::Orientation)orientation);
    }

    EXPORT void QSplitter_SetHandleWidth(void* splitter, int width) {
        ((QSplitter*)splitter)->setHandleWidth(width);
    }

    EXPORT void QSplitter_SetStretchFactor(void* splitter, int index, int stretch) {
        ((QSplitter*)splitter)->setStretchFactor(index, stretch);
    }

    EXPORT void QSplitter_SetSplitterDistance(void* splitter, int distance, int widgetSize) {
        QSplitter* s = (QSplitter*)splitter;
        QList<int> sizes = s->sizes();
        if (sizes.size() >= 2) {
            int total = 0;
            for (int size : sizes) total += size;
            
            if (total > 0) {
                // Widget has been laid out, use setSizes
                QList<int> newSizes;
                newSizes.append(distance);
                newSizes.append(total - distance);
                s->setSizes(newSizes);
            } else {
                int stretch1 = distance;
                int stretch2 = widgetSize - distance;
                if (stretch1 > 0 && stretch2 > 0) {
                    s->setStretchFactor(0, stretch1);
                    s->setStretchFactor(1, stretch2);
                }
            }
        }
    }

    EXPORT void QSplitter_ConnectSplitterMoved(void* splitter, void (*callback)(void*, int, int), void* userData) {
        QObject::connect((QSplitter*)splitter, &QSplitter::splitterMoved, [callback, userData](int pos, int index) {
            callback(userData, pos, index);
        });
    }

    EXPORT void QSplitter_GetSizes(void* splitter, int* size1, int* size2) {
        QSplitter* s = (QSplitter*)splitter;
        QList<int> sizes = s->sizes();
        if (sizes.size() >= 2) {
            *size1 = sizes[0];
            *size2 = sizes[1];
        } else {
            *size1 = 0;
            *size2 = 0;
        }
    }

    EXPORT void QWidget_GetSize(void* widget, int* width, int* height) {
        QWidget* w = (QWidget*)widget;
        QSize size = w->size();
        *width = size.width();
        *height = size.height();
    }

    // Form Modal Dialog Support
    static QMap<QWidget*, QEventLoop*> g_dialogLoops;

    EXPORT void Form_ShowDialog(void* handle) {
        QWidget* w = (QWidget*)handle;
        w->setWindowModality(Qt::ApplicationModal);
        w->show();
        
        QEventLoop loop;
        g_dialogLoops.insert(w, &loop);
        loop.exec();
        g_dialogLoops.remove(w);
        
        w->setWindowModality(Qt::NonModal);
    }

    EXPORT void Form_EndDialog(void* handle) {
        QWidget* w = (QWidget*)handle;
        if (g_dialogLoops.contains(w)) {
            g_dialogLoops[w]->quit();
        }
    }

    EXPORT void Form_SetOwner(void* child, void* owner) {
        QWidget* c = (QWidget*)child;
        QWidget* o = (QWidget*)owner;
        // Set parent with Qt::Window flag to keep it as a top-level window but owned
        c->setParent(o, Qt::Window);
    }

    EXPORT void QPushButton_Click(void* button) {
        QPushButton* btn = (QPushButton*)button;
        btn->click();
    }

    // Event filter for handling Accept/Cancel buttons
    class AcceptCancelEventFilter : public QObject {
    private:
        void* acceptButton;
        void* cancelButton;

    public:
        AcceptCancelEventFilter(void* accept, void* cancel)
            : acceptButton(accept), cancelButton(cancel) {}

        void setAcceptButton(void* accept) { acceptButton = accept; }
        void setCancelButton(void* cancel) { cancelButton = cancel; }

    protected:
        bool eventFilter(QObject* obj, QEvent* event) override {
            if (event->type() == QEvent::KeyPress) {
                QKeyEvent* keyEvent = static_cast<QKeyEvent*>(event);
                
                // Handle Enter/Return key for AcceptButton
                if ((keyEvent->key() == Qt::Key_Return || keyEvent->key() == Qt::Key_Enter) && acceptButton) {
                    QPushButton* btn = (QPushButton*)acceptButton;
                    if (btn->isEnabled() && btn->isVisible()) {
                        btn->click();
                        return true; // Event handled
                    }
                }
                
                // Handle Escape key for CancelButton
                if (keyEvent->key() == Qt::Key_Escape && cancelButton) {
                    QPushButton* btn = (QPushButton*)cancelButton;
                    if (btn->isEnabled() && btn->isVisible()) {
                        btn->click();
                        return true; // Event handled
                    }
                }
            }
            return QObject::eventFilter(obj, event);
        }
    };

    static QMap<QWidget*, AcceptCancelEventFilter*> g_acceptCancelFilters;

    EXPORT void QWidget_SetAcceptCancelButtons(void* form, void* acceptButton, void* cancelButton) {
        QWidget* w = (QWidget*)form;
        
        // Get or create the event filter for this form
        AcceptCancelEventFilter* filter = g_acceptCancelFilters.value(w, nullptr);
        if (!filter) {
            filter = new AcceptCancelEventFilter(acceptButton, cancelButton);
            w->installEventFilter(filter);
            g_acceptCancelFilters.insert(w, filter);
        } else {
            filter->setAcceptButton(acceptButton);
            filter->setCancelButton(cancelButton);
        }
    }

    // DataGridView / QTableWidget Support
    EXPORT void* QTableWidget_Create(void* parent) {
        QTableWidget* table = new QTableWidget((QWidget*)parent);
        table->setEditTriggers(QAbstractItemView::NoEditTriggers); // Read-only by default
        table->setSelectionBehavior(QAbstractItemView::SelectRows);
        return table;
    }

    EXPORT void QTableWidget_SetRowCount(void* table, int count) {
        ((QTableWidget*)table)->setRowCount(count);
    }

    EXPORT int QTableWidget_GetRowCount(void* table) {
        return ((QTableWidget*)table)->rowCount();
    }

    EXPORT void QTableWidget_SetColumnCount(void* table, int count) {
        ((QTableWidget*)table)->setColumnCount(count);
    }

    EXPORT int QTableWidget_GetColumnCount(void* table) {
        return ((QTableWidget*)table)->columnCount();
    }

    EXPORT void QTableWidget_SetCellText(void* table, int row, int column, const char16_t* text) {
        QTableWidget* t = (QTableWidget*)table;
        QTableWidgetItem* item = t->item(row, column);
        if (!item) {
            item = new QTableWidgetItem();
            t->setItem(row, column, item);
        }
        item->setText(QString::fromUtf16(text));
    }

    EXPORT void QTableWidget_GetCellText_Invoke(void* table, int row, int column, ReadQStringCallback cb, void* userData) {
        QTableWidget* t = (QTableWidget*)table;
        QTableWidgetItem* item = t->item(row, column);
        QString text = item ? item->text() : QString();
        cb((const void*)text.constData(), text.size(), userData);
    }

    EXPORT void QTableWidget_SetColumnHeaderText(void* table, int column, const char16_t* text) {
        QTableWidget* t = (QTableWidget*)table;
        QTableWidgetItem* header = new QTableWidgetItem(QString::fromUtf16(text));
        t->setHorizontalHeaderItem(column, header);
    }

    EXPORT void QTableWidget_GetColumnHeaderText_Invoke(void* table, int column, ReadQStringCallback cb, void* userData) {
        QTableWidget* t = (QTableWidget*)table;
        QTableWidgetItem* header = t->horizontalHeaderItem(column);
        QString text = header ? header->text() : QString();
        cb((const void*)text.constData(), text.size(), userData);
    }

    EXPORT void QTableWidget_InsertRow(void* table, int row) {
        ((QTableWidget*)table)->insertRow(row);
    }

    EXPORT void QTableWidget_RemoveRow(void* table, int row) {
        ((QTableWidget*)table)->removeRow(row);
    }

    EXPORT void QTableWidget_InsertColumn(void* table, int column) {
        ((QTableWidget*)table)->insertColumn(column);
    }

    EXPORT void QTableWidget_RemoveColumn(void* table, int column) {
        ((QTableWidget*)table)->removeColumn(column);
    }

    class VirtualModeDelegate : public QStyledItemDelegate {
    private:
        void (*dataNeededCallback)(void*, int, int);
        void* userData;

    public:
        VirtualModeDelegate(void (*callback)(void*, int, int), void* data, QObject* parent = nullptr)
            : QStyledItemDelegate(parent), dataNeededCallback(callback), userData(data) {}

        void paint(QPainter* painter, const QStyleOptionViewItem& option, const QModelIndex& index) const override {
            // Request data before painting if callback is set
            if (dataNeededCallback) {
                dataNeededCallback(userData, index.row(), index.column());
            }
            QStyledItemDelegate::paint(painter, option, index);
        }
    };

    EXPORT void QTableWidget_ConnectCellDataNeeded(void* table, void (*callback)(void*, int, int), void* userData) {
        QTableWidget* t = (QTableWidget*)table;
        VirtualModeDelegate* delegate = new VirtualModeDelegate(callback, userData, t);
        t->setItemDelegate(delegate);
    }

    EXPORT void QTreeWidgetItem_SetForeColor(void* item, int column, unsigned char r, unsigned char g, unsigned char b, unsigned char a) {
        QTreeWidgetItem* i = (QTreeWidgetItem*)item;
        i->setForeground(column, QBrush(QColor(r, g, b, a)));
    }

    EXPORT void QTreeWidgetItem_SetBackColor(void* item, int column, unsigned char r, unsigned char g, unsigned char b, unsigned char a) {
        QTreeWidgetItem* i = (QTreeWidgetItem*)item;
        i->setBackground(column, QBrush(QColor(r, g, b, a)));
    }

    EXPORT void QTreeWidgetItem_ClearForeColor(void* item, int column) {
        QTreeWidgetItem* i = (QTreeWidgetItem*)item;
        i->setData(column, Qt::ForegroundRole, QVariant());
    }

    EXPORT void QTreeWidgetItem_ClearBackColor(void* item, int column) {
        QTreeWidgetItem* i = (QTreeWidgetItem*)item;
        i->setData(column, Qt::BackgroundRole, QVariant());
    }

    EXPORT void QClipboard_SetText(const char16_t* text) {
        QClipboard* clipboard = QApplication::clipboard();
        clipboard->setText(QString::fromUtf16(text));
    }

    EXPORT void QClipboard_GetText_Invoke(ReadQStringCallback cb, void* userData) {
        QClipboard* clipboard = QApplication::clipboard();
        QString text = clipboard->text();
        cb((const void*)text.constData(), text.size(), userData);
    }

    EXPORT void QClipboard_Clear() {
        QClipboard* clipboard = QApplication::clipboard();
        clipboard->clear();
    }
}

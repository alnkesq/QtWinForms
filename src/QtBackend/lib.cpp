#include <QApplication>
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
#include <QProgressBar>
#include <QRadioButton>
#include <QMenuBar>
#include <QMenu>
#include <QAction>
#include <QComboBox>
#include <QFileDialog>
#include <QDoubleSpinBox>
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

    EXPORT void QWidget_Destroy(void* widget) {
        delete (QWidget*)widget;
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
}

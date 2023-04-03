#ifndef LOGICGATELIST_H
#define LOGICGATELIST_H

#include <QListWidget>
#include <QMouseEvent>
#include <QDrag>
#include <QMimeData>
#include <vector>

using std::vector;

class LogicGateList : public QListWidget
{
    Q_OBJECT

public:
    /**
     * @brief Initialize the logic gate resource list
     */
    LogicGateList(QWidget *parent = nullptr);

public slots:
    /**
     * @brief Creates all of the resources for the icons.
     * @param parent
     */
    void SetGates(vector<QString>);
};

#endif // LOGICGATELIST_H

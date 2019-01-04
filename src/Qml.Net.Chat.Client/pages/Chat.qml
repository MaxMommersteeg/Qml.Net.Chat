import QtQuick 2.11
import QtQuick.Controls 2.4
import QtQuick.Controls.Material 2.4
import QtQuick.Layouts 1.3
import ChatClient 1.0

ScrollablePage {
	id: scrollPage

	width: parent.width

    ColumnLayout {
        width: parent.width

			Column {
			spacing: 6

			Repeater {
					id: repeater
					model: Net.toListModel(ctrl.chatMessages)

					Text {
						id: chatMessage
						color: "white"
						text: modelData.user + ": '" + modelData.message + "' at " + ctrl.toLocalDateTimeString(modelData.timestamp)
					}
			}
		}

		TextField {
			id: txtMessage
			Layout.alignment: Qt.AlignBottom
			placeholderText: "Enter message"
			Layout.fillWidth: true
			Keys.onReturnPressed: ctrl.sendMessageToServer(txtMessage.text)
			Keys.onEnterPressed: ctrl.sendMessageToServer(txtMessage.text)
		}

		ChatController {
			id: ctrl

			Component.onCompleted: {
				// Connect to server.
				Net.await(ctrl.connect(), function() {
					console.log("Connected to server.")
				});
			}

			function sendMessageToServer() {
				if (txtMessage.text !== "") {
					Net.await(ctrl.sendMessage(txtMessage.text), function() {
						console.log("Sent message to server.")
						txtMessage.text = null
					})
				}
			}
        }
    }	
}

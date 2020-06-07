import numpy as np
import torch
import torchvision
import matplotlib.pyplot as plt
from time import time
from torchvision import datasets, transforms
from torch import nn, optim
from torch.utils.data import random_split, DataLoader

from dataset import EyeDataset

input_size = 100 * 50 * 2
hidden_sizes = [128, 64]
output_size = 4

dataset = EyeDataset()
train_size = int(0.8 * len(dataset))
test_size = len(dataset) - train_size
train_set, test_set = random_split(dataset, [train_size, test_size])


model = nn.Sequential(nn.Linear(input_size, hidden_sizes[0]),
                      nn.ReLU(),
                      nn.Linear(hidden_sizes[0], hidden_sizes[1]),
                      nn.ReLU(),
                      nn.Linear(hidden_sizes[1], output_size),
                      nn.LogSoftmax(dim=1))

trainloader = DataLoader(train_set, shuffle=True, num_workers=4, batch_size=64)
valloader = DataLoader(test_set, shuffle=True, num_workers=4, batch_size=64)

criterion = nn.NLLLoss()
images, labels = next(iter(trainloader))
images = images.view(images.shape[0], -1)

logps = model(images)
loss = criterion(logps, labels)

figure = plt.figure()
num_of_images = 60

dataiter = iter(trainloader)
images, labels = dataiter.next()

optimizer = optim.SGD(model.parameters(), lr=0.003, momentum=0.9)
time0 = time()
epochs = 120
print(model)
for e in range(epochs):
    running_loss = 0
    for images, labels in trainloader:
        images = images.view(images.shape[0], -1)
        optimizer.zero_grad()
        output = model(images)
        loss = criterion(output, labels)
        loss.backward()
        # And optimizes its weights here
        optimizer.step()

        running_loss += loss.item()
    else:
        print("Epoch {} - Training loss: {}".format(e, running_loss / len(trainloader)))

print("\nTraining Time (in minutes) =", (time() - time0) / 60)

correct_count, all_count = 0, 0

plt.show()
sum_of_pred = 0
labels_len = len(labels)

for images, labels in valloader:
    for i in range(len(labels)):
        img = images[i].view(1, 10000)
        with torch.no_grad():
            t = time()
            logps = model(img)
            t1 = time()
            sum_of_pred += (t1 - t)

        ps = torch.exp(logps)
        probab = list(ps.numpy()[0])
        pred_label = probab.index(max(probab))
        true_label = labels.numpy()[i]
        if (true_label == pred_label):
            correct_count += 1
        all_count += 1
print("Single prediction measurement time [s] = ", sum_of_pred / labels_len)
# print("Number Of Images Tested =", all_count)
print("\nModel Accuracy =", (correct_count / all_count))
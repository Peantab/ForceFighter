import glob
import os
from random import random, uniform

import torch
from PIL import Image
from torch.utils.data import Dataset
from torchvision.transforms import transforms
from torchvision.datasets import MNIST


class EyeDataset(Dataset):
    def __init__(self):
        self.labels = {'LU': 0, 'RU': 1, 'LD': 2, 'RD': 3}
        self.pictures = []
        for label in self.labels:
            for picture in sorted((list(glob.glob('images/{}/*.*'.format(label))))):
                self.pictures.append((picture, label))

    def __getitem__(self, index):
        img_l = Image.open(self.pictures[index * 2][0])
        img_r = Image.open(self.pictures[index * 2 + 1][0])
        label = self.labels[self.pictures[index][1]]

        tfms = transforms.Compose([
            transforms.ToTensor(),
            transforms.Normalize((0.5,), (0.5,))
        ])
        return torch.cat((tfms(img_l), tfms(img_r)), dim=1), torch.tensor(label)

    def __len__(self):
        return len(self.pictures) // 2
